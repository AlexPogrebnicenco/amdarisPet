using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineCoursesPlatform.API.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string format = "yyyy-MM-ddTHH:mm:ssZ"; // ISO 8601

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString(format));
        }
    }
}
