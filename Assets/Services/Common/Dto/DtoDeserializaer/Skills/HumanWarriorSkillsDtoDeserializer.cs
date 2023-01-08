using System;
using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanWarriorSkillsDtoDeserializer : UnitSkillsDeserializerBase,
        IDtoDeserializer<HumanWarriorSkillsDto>
    {
        public override SkillsDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var jObject = JObject.Parse(json);
            return new HumanWarriorSkillsDto
            {
                id = jObject["id"]?.Value<long?>() ?? -1,
                unitId = jObject["unitId"]?.Value<long?>() ?? -1,
                defencePoints = jObject["defencePoints"]?.Value<long>() ?? -1,
                offencePoints = jObject["offencePoints"]?.Value<long>() ?? -1
            };
        }

        HumanWarriorSkillsDto IDtoDeserializer<HumanWarriorSkillsDto>.Deserialize(string json)
        {
            return (HumanWarriorSkillsDto)Deserialize(json);
        }
    }
}