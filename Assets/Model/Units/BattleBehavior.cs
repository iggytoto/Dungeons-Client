using System;

namespace Model.Units
{
    [Serializable]
    public enum BattleBehavior
    {
        GuardNearestAlly,
        DoNothing,
        StraightAttack,
        Panic,
        HitAndRun,
        AttackBackLines
    }
}