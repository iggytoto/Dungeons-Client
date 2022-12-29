using System;
using Model.Units;

namespace Services.Common.Dto
{
    public abstract class UnitEquipmentDeserializerBase
    {
        public static UnitEquipmentDeserializerBase GetDeserializer(UnitType type) 
        {
            return type switch
            {
                UnitType.Dummy => null,
                UnitType.HumanWarrior =>  new HumanWarriorEquipmentDtoDeserializer(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public abstract EquipmentDto Deserialize(string json);
    }
}