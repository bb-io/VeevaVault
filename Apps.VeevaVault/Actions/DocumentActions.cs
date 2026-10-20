using System.Net.Mime;
using Apps.VeevaVault.Api;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Common;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.Document;
using Apps.VeevaVault.Models.Responses;
using Apps.VeevaVault.Models.Responses.Document;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.VeevaVault.Actions;

[ActionList("Documents")]
public class DocumentActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
    : VeevaVaultInvocable(invocationContext)
{
    [Action("Search documents", Description = "Search documents with a specific criteria")]
    public async Task<SearchDocumentsResponse> SearchDocuments(
        [ActionParameter] SearchDocumentsRequest input)
    {
        List<long>? documentIds = null;
        if (!string.IsNullOrEmpty(input.BinderId))
        {
            documentIds = await Client.GetBinderDocumentIds(input.BinderId);
            if (documentIds.Count == 0)
                return new([]);
        }
        
        if (!string.IsNullOrEmpty(input.WorkflowStep))
        {
            var stepIds = await Client.GetWorkflowStepDocumentIds(input.WorkflowStep);
            if (stepIds.Count == 0)
                return new([]);

            documentIds = documentIds is null ? stepIds : documentIds.Intersect(stepIds).ToList();
            if (documentIds.Count == 0)
                return new([]);
        }
        
        string vql = new VqlBuilder("documents", VqlFields.Document)
            .Find(input.NameContains, "name__v")
            .Where("binder__v = false")
            .WhereContains("TONAME(type__v)", input.DocumentTypes)
            .WhereContains("id", documentIds)
            .Build();
        
        var response = await Client.Query<DocumentEntity>(vql);
        return new(response.Select(x => new DocumentResponse(x)).ToList());
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-documents/retrieve-document/
    [Action("Get document", Description = "Get details for a specific document")]
    public async Task<DocumentResponse> GetDocument([ActionParameter] DocumentIdentifier documentIdentifier)
    {
        var response = await Client.GetDocument(documentIdentifier.DocumentId);
        return new(response);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/update-documents/update-single-document/
    [Action("Update document", Description = "Update fields of a specific document")]
    public Task UpdateDocument(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] UpdateDocumentRequest input)
    {
        input.Validate();

        var request = new VeevaRequest($"objects/documents/{documentIdentifier.DocumentId}", Method.Put);
        foreach (var (name, value) in input.DocumentFieldNames.Zip(input.DocumentFieldValues))
            request.AddParameter(name, value);

        return Client.ExecuteWithErrorHandling(request);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/document-attachments/download-document-attachment-version/
    [Action("Download document attachment", Description = "Download the latest version of the specified attachment from the document")]
    public async Task<FileResponse> DownloadDocumentAttachment(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] DocumentAttachmentIdentifier attachmentIdentifier)
    {
        string endpoint = $"objects/documents/{documentIdentifier.DocumentId}/attachments/{attachmentIdentifier.AttachmentId}/file";
        var file = await DownloadFileReference(endpoint);
        return new(file);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/document-attachments/create-document-attachment/
    [Action("Upload document attachment", Description = "Create and upload an attachment on the latest version of a document")]
    public async Task<UploadDocumentAttachmentResponse> UploadDocumentAttachment(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] UploadDocumentAttachmentRequest input)
    {
        await using var fileStream = await fileManagementClient.DownloadAsync(input.File);
        var fileBytes = await fileStream.GetByteData();
        
        var request = new VeevaRequest($"objects/documents/{documentIdentifier.DocumentId}/attachments", Method.Post)
            .AddFile("file", fileBytes, input.File.Name);

        var response = await Client.ExecuteWithErrorHandling<DataArrayWrapper<CreateDocumentAttachmentResponse>>(request);
        return new(response.Data.First());
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-documents/download-document-file/
    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-documents/download-document-version-file/
    [Action("Download document", Description = "Download a specific document file")]
    public async Task<FileResponse> DownloadDocument(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] DownloadDocumentRequest input)
    {
        string endpoint = !string.IsNullOrEmpty(input.Version) 
            ? $"objects/documents/{documentIdentifier.DocumentId}/{DocumentVersionHelper.ToVersionPath(input.Version)}/file" 
            : $"objects/documents/{documentIdentifier.DocumentId}/file";
        
        var file = await DownloadFileReference(endpoint);
        return new(file);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/create-documents/create-single-document/
    [Action("Create document", Description = "Create a new document from a file")]
    public async Task<DocumentResponse> CreateDocument(
        [ActionParameter] DocumentTypeIdentifier documentTypeIdentifier,
        [ActionParameter] CreateDocumentRequest input)
    {
        await using var fileStream = await fileManagementClient.DownloadAsync(input.File);
        var fileBytes = await fileStream.GetByteData();
        
        var request = new VeevaRequest("objects/documents", Method.Post) { AlwaysMultipartFormData = true }
            .AddFile("file", fileBytes, input.File.Name)
            .AddParameter("name__v", input.CustomName ?? Path.GetFileNameWithoutExtension(input.File.Name))
            .AddParameter("type__v", documentTypeIdentifier.DocumentType)
            .AddParameter("lifecycle__v", input.Lifecycle)
            .AddParameterIfNotEmpty("subtype__v", input.Subtype);
        var response = await Client.ExecuteWithErrorHandling<CreateSingleDocumentResponse>(request);
        
        var createdDocumentEntity = await Client.GetDocument(response.Id.ToString());
        return new(createdDocumentEntity);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/document-relationships/retrieve-document-type-relationships/
    [Action("Set document relationship", Description = "Create a new relationship on a document")]
    public async Task<SetDocumentRelationshipResponse> SetDocumentRelationship(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] DocumentVersionIdentifier documentVersionIdentifier,
        [ActionParameter] SetDocumentRelationshipRequest input)
    {
        string version = DocumentVersionHelper.ToVersionPath(documentVersionIdentifier.DocumentVersion);
        string endpoint = $"objects/documents/{documentIdentifier.DocumentId}/{version}/relationships";

        var request = new VeevaRequest(endpoint, Method.Post)
            .AddParameter("target_doc_id__v", input.TargetDocumentId)
            .AddParameter("relationship_type__v", input.RelationshipType);
        var response = await Client.ExecuteWithErrorHandling<CreateSingleDocumentRelationshipResponse>(request);
        return new(response);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.1/document-lifecycle-workflows/document-user-actions/initiate-document-user-action/
    [Action("Start document action", Description = "Initiate a user action")]
    public Task StartDocumentAction(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] DocumentVersionIdentifier documentVersionIdentifier,
        [ActionParameter] DocumentUserActionIdentifier actionIdentifier)
    {
        string version = DocumentVersionHelper.ToVersionPath(documentVersionIdentifier.DocumentVersion);
        string endpoint = $"objects/documents/{documentIdentifier.DocumentId}/{version}/lifecycle_actions/{actionIdentifier.ActionName}";
        
        var request = new VeevaRequest(endpoint, Method.Put);
        return Client.ExecuteWithErrorHandling(request);
    }

    private async Task<FileReference> DownloadFileReference(string endpoint)
    {
        var request = new VeevaRequest(endpoint);
        var response = await Client.ExecuteWithErrorHandling(request);

        string contentType = response.ContentType ?? MediaTypeNames.Application.Octet;
        string fileName = response.GetFilenameFromDisposition();

        var responseBytes = response.RawBytes ?? throw new PluginApplicationException("Vault returned 0 bytes");
        using var memoryStream = new MemoryStream(responseBytes);

        return await fileManagementClient.UploadAsync(memoryStream, contentType, fileName);
    }
}