using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class CheckIfTargetInRadiusTargetAllyTask : UnitTaskBase
    {
        private readonly float _radius;

        public CheckIfTargetInRadiusTargetAllyTask(UnitStateController unitStateController, float radius) : base(
            unitStateController)
        {
            _radius = radius;
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            var target = GetTarget();
            var targetAlly = GetTargetAlly();
            if (target == null || targetAlly == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            if (Vector3.Distance(target.transform.position, targetAlly.transform.position) <= _radius)
            {
                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }
    }
}