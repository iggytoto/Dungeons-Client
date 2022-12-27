using DefaultNamespace.Animation;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class MoveToTargetBattleTask : UnitTaskBase
    {
        public MoveToTargetBattleTask(UnitStateController unitStateController) : base(unitStateController)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            var target = (UnitStateController)GetData("target");
            if (target == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            if (Vector3.Distance(Unit.transform.position, target.transform.position) >=
                Unit.GetCurrentAttackRange())
            {
                var position = target.transform.position;
                Unit.transform.position = Vector3.MoveTowards(
                    Unit.transform.position,
                    position,
                    Unit.GetCurrentSpeed() * Time.deltaTime);
                Unit.transform.LookAt(position);
                Animator.SetBool(AnimationConstants.IsRunningBoolean, true);
            }
            else
            {
                Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }
    }
}