using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanArcherSkillsDtoDeserializer : UnitSkillsDeserializerBase,
        IDtoDeserializer<HumanArcherSkillsDto>
    {
        public override SkillsDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var jObject = JObject.Parse(json);
            return new HumanArcherSkillsDto
            {
                id = jObject["id"]?.Value<long?>() ?? -1,
                unitId = jObject["unitId"]?.Value<long?>() ?? -1,
                midRangePoints = jObject["midRangePoints"]?.Value<long>() ?? -1,
                longRangePoints = jObject["longRangePoints"]?.Value<long>() ?? -1,
                fireArrows = jObject["fireArrows"]?.Value<bool>() ?? false,
                poisonArrows = jObject["poisonArrows"]?.Value<bool>() ?? false
            };
        }

        HumanArcherSkillsDto IDtoDeserializer<HumanArcherSkillsDto>.Deserialize(string json)
        {
            return (HumanArcherSkillsDto)Deserialize(json);
        }
    }
}