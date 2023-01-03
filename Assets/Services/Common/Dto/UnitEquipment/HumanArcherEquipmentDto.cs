using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanArcherEquipmentDto : EquipmentDto
    {
        public long midRangePoints;
        public long longRangePoints;
        public bool fireArrows;
        public bool poisonArrows;


        public override Equipment ToDomain()
        {
            return new HumanArcherEquipment
            {
                id = id,
                unitId = unitId,
                fireArrows = fireArrows,
                poisonArrows = poisonArrows,
                longRangePoints = longRangePoints,
                midRangePoints = midRangePoints
            };
        }
    }
}