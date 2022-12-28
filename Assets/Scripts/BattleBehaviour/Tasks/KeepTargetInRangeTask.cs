using DefaultNamespace.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class KeepTargetInRangeTask : NavMeshAgentUnitTaskBase
    {
        public KeepTargetInRangeTask(
            UnitStateController unitStateController,
            NavMeshAgent navMeshAgent) : base(unitStateController, navMeshAgent)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            var target = GetTarget();
            if (target == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            var distance = Vector3.Distance(Unit.transform.position, target.transform.position);
            var ar = Unit.GetCurrentAttackRange();
            var minRadius = ar * .9;

            if (minRadius <= distance && distance <= ar)
            {
                Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
                State = BattleBehaviorNodeState.Success;
                return State;
            }


            Vector3 destination;
            Animator.SetBool(AnimationConstants.IsRunningBoolean, true);
            if (minRadius >= distance)
            {
                destination = Unit.transform.position + Vector3.back;
            }
            else
            {
                destination = target.transform.position;
            }

            RecalculatePath(destination);
            Move();

            State = BattleBehaviorNodeState.Running;
            return State;
        }
    }
}