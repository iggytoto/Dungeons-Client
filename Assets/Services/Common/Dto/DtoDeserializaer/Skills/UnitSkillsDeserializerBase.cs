using System;
using Model.Units;
using Services.Dto;

namespace Services.Common.Dto
{
    public abstract class UnitSkillsDeserializerBase
    {
        public static IDtoDeserializer<T> GetDeserializer<T>(UnitType type) where T : ResponseBaseDto
        {
            return (IDtoDeserializer<T>)GetDeserializer(type);
        }

        public static UnitSkillsDeserializerBase GetDeserializer(UnitType type)
        {
            return type switch
            {
                UnitType.Dummy => null,
                UnitType.HumanArcher => new HumanArcherSkillsDtoDeserializer(),
                UnitType.HumanWarrior => new HumanWarriorSkillsDtoDeserializer(),
                UnitType.HumanSpearman => new HumanSpearmanSkillsDtoDeserializer(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public abstract SkillsDto Deserialize(string json);
    }
}