using System.Linq;
using DefaultNamespace.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class MoveToTargetAllyInRangeTask: NavMeshAgentUnitTaskBase
    {
        private UnitStateController _targetAlly;
        private readonly float _range;

        public MoveToTargetAllyInRangeTask(UnitStateController unitStateStateController, NavMeshAgent navMeshAgent,float range) : base(
            unitStateStateController,navMeshAgent)
        {
            _range = range;
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            _targetAlly = GetTargetAlly();
            if (_targetAlly == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            RecalculatePath(_targetAlly.transform.position);
            if (Path.Any())
            {
                var lastPathPoint = Path.Last();

                if (Vector3.Distance(UnitState.transform.position, lastPathPoint) >=
                    _range)
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