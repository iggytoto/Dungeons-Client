using System;
using Model.Units;
using Services.Dto;

namespace Services.Common.Dto
{
    public abstract class UnitEquipmentDeserializerBase
    {
        public static IDtoDeserializer<T> GetDeserializer<T>(UnitType type) where T : ResponseBaseDto
        {
            return (IDtoDeserializer<T>)GetDeserializer(type);
        }

        public static UnitEquipmentDeserializerBase GetDeserializer(UnitType type)
        {
            return type switch
            {
                UnitType.Dummy => null,
                UnitType.HumanArcher => new HumanArcherEquipmentDtoDeserializer(),
                UnitType.HumanWarrior => new HumanWarriorEquipmentDtoDeserializer(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public abstract EquipmentDto Deserialize(string json);
    }
}