using System;

namespace Model.Units
{
    [Serializable]
    public enum BattleBehavior
    {
        DoNothing,
        GuardNearestAlly,
        StraightAttack,
        Panic,
        HitAndRun,
        AttackBackLines
    }
}