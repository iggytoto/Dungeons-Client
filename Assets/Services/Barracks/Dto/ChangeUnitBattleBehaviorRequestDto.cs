using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class ChangeUnitBattleBehaviorRequestDto : RequestDto
    {
        public long unitId;
        public BattleBehavior newBattleBehavior;
    }
}