using System;

namespace DefaultNamespace.BattleBehaviour
{
    public static class BattleBehaviourManager
    {
        public static void UpdateBattleBehaviour(UnitController unitController)
        {
            switch (unitController.Unit.BattleBehaviour)
            {
                case Model.Units.BattleBehaviour.DoNothing:
                    unitController.gameObject.AddComponent<DoNothingBattleBehaviour>();
                    break;
                case Model.Units.BattleBehaviour.StraightAttack:
                    unitController.gameObject.AddComponent<AttackClosestEnemyBattleBehaviour>();
                    break;
                case Model.Units.BattleBehaviour.Panic:
                    unitController.gameObject.AddComponent<PanicBattleBehavior>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}