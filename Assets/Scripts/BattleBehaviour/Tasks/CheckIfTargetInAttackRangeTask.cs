using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class CheckIfTargetInAttackRangeTask : BattleBehaviorNode
    {
        private readonly UnitStateController _unit;

        public CheckIfTargetInAttackRangeTask(UnitStateController unit)
        {
            _unit = unit;
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            var target = (UnitStateController)GetData("target");
            if (target == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            if (Vector3.Distance(_unit.transform.position, target.transform.position) <= _unit.AttackRange)
            {
                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }
    }
}