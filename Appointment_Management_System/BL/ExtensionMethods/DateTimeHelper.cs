using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Appointment_Management_System.BL.ExtensionMethods
{
    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {  
            return DateTime.Parse(reader.GetString()!).ToUniversalTime();
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {  
            value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            writer.WriteStringValue(value.ToUniversalTime().ToString("o"));
        }
    }
}
