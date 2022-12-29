using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class ChangeUnitBattleBehaviorRequestDto
    {
        public long unitId;
        public BattleBehavior newBattleBehavior;
    }
}