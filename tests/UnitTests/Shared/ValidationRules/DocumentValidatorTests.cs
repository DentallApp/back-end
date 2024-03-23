namespace UnitTests.Shared.ValidationRules;

public class DocumentValidatorTests
{
    [Test]
    public void ShouldHaveErrorWhenDocumentIsInvalid()
    {
        var userValidator = new UserValidator(new IdentityDocumentValidator());
        var model = new User { Document = "2523611701" };
        var result = userValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(user => user.Document);
    }

    [Test]
    public void ShouldNotHaveErrorWhenDocumentIsValid()
    {
        var userValidator = new UserValidator(new IdentityDocumentValidator());
        var model = new User { Document = "1311982324" };
        var result = userValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(user => user.Document);
    }

    [TestCase("")]
    [TestCase("  ")]
    [TestCase(null)]
    public void ShouldHaveErrorWhenDocumentIsEmpty(string document)
    {
        var userValidator = new UserValidator(new IdentityDocumentValidator());
        var model = new User { Document = document };
        var result = userValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(user => user.Document);
    }

    private class User
    {
        public string Document { get; init; }
    }

    private class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IIdentityDocumentValidator documentValidator)
        {
            RuleFor(user => user.Document)
                .MustBeValidIdentityDocument(documentValidator);
        }
    }
}
