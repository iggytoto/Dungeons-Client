using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanSpearmanEquipmentDto : EquipmentDto
    {
        public long doubleEdgePoints;
        public long midRangePoints;

        public override Equipment ToDomain()
        {
            return new HumanSpearmanEquipment()
            {
                id = id,
                unitId = unitId,
                doubleEdgePoints = doubleEdgePoints,
                midRangePoints = midRangePoints
            };
        }
    }
}