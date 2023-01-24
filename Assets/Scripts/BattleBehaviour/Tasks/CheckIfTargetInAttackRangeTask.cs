namespace DefaultNamespace.BattleBehaviour
{
    public class CheckIfTargetInAttackRangeTask : UnitTaskBase
    {
        public CheckIfTargetInAttackRangeTask(UnitStateController unit) : base(unit)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            var target = GetTarget();
            if (target == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            if (IsWithinAttackRange(target))
            {
                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }
    }
}