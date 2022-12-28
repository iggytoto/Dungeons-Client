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
                case Model.Units.BattleBehavior.DoNothing:
                    unitStateController.gameObject.AddComponent<DoNothingBattleBehaviour>();
                    break;
                case Model.Units.BattleBehavior.StraightAttack:
                    unitStateController.gameObject.AddComponent<AttackClosestEnemyBattleBehavior>();
                    break;
                case Model.Units.BattleBehavior.Panic:
                    unitStateController.gameObject.AddComponent<PanicBattleBehavior>();
                    break;
                case BattleBehavior.GuardNearestAlly:
                    unitStateController.gameObject.AddComponent<GuardNearestAllyBattleBehavior>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}