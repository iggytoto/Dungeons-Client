using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class UpgradeUnitSkillRequestDto
    {
        public long skillsId;
        public UnitType unitType;
        public string paramNameToUpgrade;
    }
}