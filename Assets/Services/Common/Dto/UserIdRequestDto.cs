using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Common.Dto;

namespace Services.Common
{
    [Serializable]
    public class UserIdRequestDto : RequestDto
    {
        [JsonConverter(typeof(IdToLongConverter))]
        public long userId;
    }

    public class IdToLongConverter : JsonConverter<long>
    {
        public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return JToken.ReadFrom(reader).Value<long>();
        }
    }
}