namespace UnitTests.Plugins.IdentityDocumentEcuador;

public class IdentityDocumentValidatorTests
{
    [TestCase("")]
    [TestCase("  ")]
    [TestCase(null)]
    public void IsValid_WhenDocumentIsEmpty_ShouldReturnsInvalidResult(string document)
    {
        // Arrange
        var validator = new IdentityDocumentValidator();
        var expectedMessage = Messages.DocumentIsEmpty;

        // Act
        Result result = validator.IsValid(document);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase("12345")]
    [TestCase("12345678910")]
    public void IsValid_WhenDocumentDoesNotHaveTenCharacters_ShouldReturnsInvalidResult(string document)
    {
        // Arrange
        var validator = new IdentityDocumentValidator();
        var expectedMessage = string.Format(Messages.DocumentMaxCharacters, 10);

        // Act
        Result result = validator.IsValid(document);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase("092361170a")]
    [TestCase("09236A1701")]
    [TestCase("09236$#a0-")]
    public void IsValid_WhenDocumentIsNotNumeric_ShouldReturnsInvalidResult(string document)
    {
        // Arrange
        var validator = new IdentityDocumentValidator();
        var expectedMessage = Messages.DocumentMustBeNumeric;

        // Act
        Result result = validator.IsValid(document);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase("0023611701")]
    [TestCase("2523611701")]
    [TestCase("2923611701")]
    [TestCase("3123611701")]
    [TestCase("0104132817")]
    [TestCase("0108875282")]
    [TestCase("0100704434")]
    [TestCase("0100201244")]
    [TestCase("0107304047")]
    [TestCase("0106784415")]
    [TestCase("0100194636")]
    [TestCase("0106354840")]
    [TestCase("0100924960")]
    [TestCase("1404840463")]
    [TestCase("1313821924")]
    public void IsValid_WhenDocumentIsInvalid_ShouldReturnsInvalidResult(string document)
    {
        // Arrange
        var validator = new IdentityDocumentValidator();
        var expectedMessage = Messages.DocumentIsInvalid;

        // Act
        Result result = validator.IsValid(document);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase("1713175071")]
    [TestCase("1710034065")]
    [TestCase("0923611701")]
    [TestCase("0102813417")]
    [TestCase("0105287882")]
    [TestCase("0104470034")]
    [TestCase("0101220044")]
    [TestCase("0104030747")]
    [TestCase("0104478615")]
    [TestCase("0104619036")]
    [TestCase("0104835640")]
    [TestCase("0104992060")]
    [TestCase("1400484463")]
    [TestCase("1500449861")]
    [TestCase("1311982324")]
    public void IsValid_WhenDocumentIsValid_ShouldReturnsSuccessResult(string document)
    {
        // Arrange
        var validator = new IdentityDocumentValidator();

        // Act
        Result result = validator.IsValid(document);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
