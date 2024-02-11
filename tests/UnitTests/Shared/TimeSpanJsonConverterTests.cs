namespace UnitTests.Shared;

public class TimeSpanJsonConverterTests
{
    class Person
    {
        [JsonConverter(typeof(TimeSpanJsonConverter))]
        public TimeSpan StartHour { get; init; }
    }

    [TestCase("08:01",    8, 1, 0)]
    [TestCase("8:1",      8, 1, 0)]
    [TestCase("10:25",    10, 25, 0)]
    [TestCase("08:01:25", 8, 1, 25)]
    [TestCase("8:1:25",   8, 1, 25)]
    public void Read_WhenDeserializingJson_ShouldConvertItToTimeSpanType(
        string startHour,
        int expectedHour,
        int expectedMinute,
        int expectedSecond)
    {
        // Arrange
        var json =
        $$"""
        {
            "StartHour": "{{startHour}}"
        }
        """;
        var expectedTimeSpan = new TimeSpan(expectedHour, expectedMinute, expectedSecond);

        // Act
        Person person = JsonSerializer.Deserialize<Person>(json);
        TimeSpan actual = person.StartHour;

        // Assert
        actual.Should().Be(expectedTimeSpan);
    }

    [TestCase(8, 1, 0, "08:01")]
    [TestCase(8, 1, 10, "08:01")]
    public void Write_WhenSerializingJson_ShouldConvertTimeSpanToHHmmFormat(
        int hour, 
        int minute,
        int second,
        string expectedStartHour)
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var person = new Person 
        { 
            StartHour = new TimeSpan(hour, minute, second) 
        };
        var expectedJson =
        $$"""
        {
          "StartHour": "{{expectedStartHour}}"
        }
        """;

        // Act
        var jsonActual = JsonSerializer.Serialize(person, options);

        // Assert
        jsonActual.Should().Be(expectedJson);
    }
}
