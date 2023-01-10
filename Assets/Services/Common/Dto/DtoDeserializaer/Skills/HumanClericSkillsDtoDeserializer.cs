using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanClericSkillsDtoDeserializer : UnitSkillsDeserializerBase,
        IDtoDeserializer<HumanClericSkillsDto>
    {
        public override SkillsDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var jObject = JObject.Parse(json);
            return new HumanClericSkillsDto
            {
                id = jObject["id"]?.Value<long?>() ?? -1,
                unitId = jObject["unitId"]?.Value<long?>() ?? -1,
                disciplinePoints = jObject["disciplinePoints"]?.Value<long>() ?? -1,
                divine = jObject["divine"]?.Value<bool>() ?? false,
                purge = jObject["purge"]?.Value<bool>() ?? false,
                shatter = jObject["shatter"]?.Value<bool>() ?? false,
            };
        }

        HumanClericSkillsDto IDtoDeserializer<HumanClericSkillsDto>.Deserialize(string json)
        {
            return (HumanClericSkillsDto)Deserialize(json);
        }
    }
}