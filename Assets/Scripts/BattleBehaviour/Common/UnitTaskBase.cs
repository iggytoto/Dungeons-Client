using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public abstract class UnitTaskBase : BattleBehaviorNode
    {
        protected const string TargetDataKey = "target";
        protected const string TargetAllyDataKey = "targetAlly";

        protected readonly Animator Animator;
        protected readonly UnitStateController Unit;

        protected UnitTaskBase(UnitStateController unitStateController)
        {
            Unit = unitStateController;
            Animator = unitStateController.gameObject.GetComponentInChildren<Animator>();
        }

        protected UnitStateController GetTarget()
        {
            return (UnitStateController)GetData(TargetDataKey);
        }

        protected bool ClearTarget()
        {
            return ClearData(TargetDataKey);
        }

        public void SetTarget(UnitStateController target)
        {
            SetData(TargetDataKey, target);
        }
        
        protected UnitStateController GetTargetAlly()
        {
            return (UnitStateController)GetData(TargetAllyDataKey);
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