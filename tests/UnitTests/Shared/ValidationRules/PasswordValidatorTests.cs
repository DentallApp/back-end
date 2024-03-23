namespace UnitTests.Shared.ValidationRules;

public class PasswordValidatorTests
{
    [TestCase("1234")]
    [TestCase("D234")]
    [TestCase("DD34")]
    [TestCase("d234")]
    [TestCase("dd34")]
    [TestCase("Da34")]
    [TestCase("DaV4")]
    [TestCase("Dav4")]
    [TestCase("dAv4")]
    [TestCase("DAVE")]
    [TestCase("dave")]
    [TestCase("DAve")]
    [TestCase("123456789")]
    [TestCase("dave12354")]
    [TestCase("DAVE12354")]
    [TestCase("daveeclop")]
    [TestCase("DAVEECLOP")]
    [TestCase("DaVeecLop")]
    public void ShouldHaveErrorWhenPasswordIsInsecure(string password)
    {
        var validator = new UserValidator();
        var model = new User { Password = password };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(user => user.Password);
    }

    [TestCase("Dsr2799")]
    [TestCase("Dsr27")]
    public void ShouldNotHaveErrorWhenPasswordIsSecure(string password)
    {
        var validator = new UserValidator();
        var model = new User { Password = password };
        var result = validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(user => user.Password);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase(null)]
    public void ShouldHaveErrorWhenPasswordIsEmpty(string password)
    {
        var validator = new UserValidator();
        var model = new User { Password = password };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(user => user.Password);
    }

    private class User
    {
        public string Password { get; init; }
    }

    private class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Password)
                .MustBeSecurePassword();
        }
    }
}
