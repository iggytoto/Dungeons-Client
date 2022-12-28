using System.Linq;
using DefaultNamespace.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class MoveToTargetBattleTask : NavMeshAgentUnitTaskBase
    {
        private UnitStateController _target;

        public MoveToTargetBattleTask(UnitStateController unitStateController, NavMeshAgent navMeshAgent) : base(
            unitStateController, navMeshAgent)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            _target = GetTarget();
            if (_target == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            RecalculatePath(_target.transform.position);
            if (Path.Any())
            {
                var lastPathPoint = Path.Last();

                if (Vector3.Distance(Unit.transform.position, lastPathPoint) >=
                    Unit.GetCurrentAttackRange())
                {
                    Move();
                    Animator.SetBool(AnimationConstants.IsRunningBoolean, true);
                }
                else
                {
                    Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
                }

                State = BattleBehaviorNodeState.Running;
                return State;
            }

            State = BattleBehaviorNodeState.Success;
            return State;
        }
    }
}