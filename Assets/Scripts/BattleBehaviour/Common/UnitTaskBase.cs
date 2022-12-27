using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class UnitTaskBase : BattleBehaviorNode
    {
        protected readonly Animator Animator;
        protected readonly UnitStateController Unit;

        public UnitTaskBase(UnitStateController unitStateController)
        {
            Unit = unitStateController;
            Animator = unitStateController.gameObject.GetComponentInChildren<Animator>();
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (Unit == null || Unit.IsDead())
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }
    }
}