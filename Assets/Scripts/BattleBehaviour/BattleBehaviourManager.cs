using System;

namespace DefaultNamespace.BattleBehaviour
{
    public static class BattleBehaviourManager
    {
        public static void UpdateBattleBehaviour(UnitStateController unitStateController)
        {
            switch (unitStateController.Unit.battleBehaviour)
            {
                case Model.Units.BattleBehaviour.DoNothing:
                    unitStateController.gameObject.AddComponent<DoNothingBattleBehaviour>();
                    break;
                case Model.Units.BattleBehaviour.StraightAttack:
                    unitStateController.gameObject.AddComponent<AttackClosestEnemyBattleBehavior>();
                    break;
                case Model.Units.BattleBehaviour.Panic:
                    unitStateController.gameObject.AddComponent<PanicBattleBehavior>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}