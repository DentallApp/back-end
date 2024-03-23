namespace DentallApp.Infrastructure.Services;

public class FakeIdentityDocument : IIdentityDocumentValidator
{
    public Result IsValid(string document) => Result.Success();
}
