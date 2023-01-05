using System.Linq;
using DefaultNamespace.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class MoveToTargetBattleTask : NavMeshAgentUnitTaskBase
    {
        private UnitStateController _target;

        public MoveToTargetBattleTask(UnitStateController unitStateStateController, NavMeshAgent navMeshAgent) : base(
            unitStateStateController, navMeshAgent)
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

                if (Vector3.Distance(UnitState.transform.position, lastPathPoint) >=
                    UnitState.AttackRange)
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