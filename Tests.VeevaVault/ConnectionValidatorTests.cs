using Apps.VeevaVault.Connections;
using Apps.VeevaVault.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Tests.Appname.Base;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class ConnectionValidatorTests : TestBase
{
    [TestMethod]
    public async Task ValidateConnection_ValidData_IsSuccess()
    {
        // Arrange
        var validator = new ConnectionValidator(InvocationContext);

        // Act
        var result = await validator.ValidateConnection(Creds, CancellationToken.None);
        
        // Assert
        Console.WriteLine(result.Message);
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public async Task ValidateConnection_InvalidVaultDns_IsFailure()
    {
        // Arrange
        var validator = new ConnectionValidator(InvocationContext);
        var newCredentials = Creds
            .Select(x => new AuthenticationCredentialsProvider(
                x.KeyName, 
                x.KeyName == CredsNames.VaultDns ? x.Value + "_incorrect" : x.Value));

        // Act
        var result = await validator.ValidateConnection(newCredentials, CancellationToken.None);
        
        // Assert
        Console.WriteLine(result.Message);
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public async Task ValidateConnection_InvalidUsernameAndPwd_IsFailure()
    {
        // Arrange
        var validator = new ConnectionValidator(InvocationContext);
        var newCredentials = Creds
            .Select(x => new AuthenticationCredentialsProvider(
                x.KeyName, 
                x.KeyName != CredsNames.VaultDns ? x.Value + "_incorrect" : x.Value));

        // Act
        var result = await validator.ValidateConnection(newCredentials, CancellationToken.None);
        
        // Assert
        Console.WriteLine(result.Message);
        Assert.IsFalse(result.IsValid);
    }
}