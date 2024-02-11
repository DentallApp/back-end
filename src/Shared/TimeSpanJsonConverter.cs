namespace DentallApp.Shared;

public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            TimeSpan.Parse(reader.GetString(), CultureInfo.InvariantCulture);

    public override void Write(
        Utf8JsonWriter writer,
        TimeSpan timeSpan,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(timeSpan.ToString(
                @"hh\:mm", CultureInfo.InvariantCulture));
}
