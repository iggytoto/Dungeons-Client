using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class UpgradeUnitEquipmentRequestDto
    {
        public long equipmentId;
        public UnitType unitType;
        public string paramNameToUpgrade;
    }
}