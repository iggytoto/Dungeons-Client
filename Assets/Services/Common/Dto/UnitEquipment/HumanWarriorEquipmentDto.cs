using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanWarriorEquipmentDto : EquipmentDto
    {
        public long defencePoints;
        public long offencePoints;

        public override Equipment ToDomain()
        {
            return new HumanWarriorEquipment
            {
                id = id,
                defencePoints = defencePoints,
                offencePoints = offencePoints,
                unitId = unitId
            };
        }
    }
}