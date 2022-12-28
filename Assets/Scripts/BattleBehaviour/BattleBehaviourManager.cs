using System;
using Model.Units;

namespace DefaultNamespace.BattleBehaviour
{
    public static class BattleBehaviourManager
    {
        public static void UpdateBattleBehaviour(UnitStateController unitStateController)
        {
            switch (unitStateController.Unit.battleBehavior)
            {
                case BattleBehavior.DoNothing:
                    unitStateController.gameObject.AddComponent<DoNothingBattleBehaviour>();
                    break;
                case BattleBehavior.StraightAttack:
                    unitStateController.gameObject.AddComponent<AttackClosestEnemyBattleBehavior>();
                    break;
                case BattleBehavior.Panic:
                    unitStateController.gameObject.AddComponent<PanicBattleBehavior>();
                    break;
                case BattleBehavior.GuardNearestAlly:
                    unitStateController.gameObject.AddComponent<GuardNearestAllyBattleBehavior>();
                    break;
                case BattleBehavior.HitAndRun:
                    unitStateController.gameObject.AddComponent<HitAndRunBattleBehavior>();
                    break;
                case BattleBehavior.AttackBackLines:
                    unitStateController.gameObject.AddComponent<AttackBackLinesBattleBehavior>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}