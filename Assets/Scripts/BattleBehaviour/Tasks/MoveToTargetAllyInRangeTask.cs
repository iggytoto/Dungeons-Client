using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class MoveToTargetAllyInRangeTask: UnitTaskBase
    {
        private readonly NavMeshAgent _navMeshAgent;
        private UnitStateController _targetAlly;
        private readonly List<Vector3> _path = new();
        private readonly float _range;

        public MoveToTargetAllyInRangeTask(UnitStateController unitStateController, NavMeshAgent navMeshAgent,float range) : base(
            unitStateController)
        {
            _navMeshAgent = navMeshAgent;
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

            RecalculatePath();
            if (_path.Any())
            {
                var lastPathPoint = _path.Last();
                var firstPathPoint = _path.First();

                if (Vector3.Distance(Unit.transform.position, lastPathPoint) >=
                    _range)
                {
                    Unit.transform.position = Vector3.MoveTowards(
                        Unit.transform.position,
                        firstPathPoint,
                        Unit.GetCurrentSpeed() * Time.deltaTime);
                    Unit.transform.LookAt(firstPathPoint);
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

        

        private void RecalculatePath()
        {
            _path.Clear();
            if (_targetAlly == null)
            {
                return;
            }

            var nmp = new NavMeshPath();
            _navMeshAgent.CalculatePath(_targetAlly.transform.position, nmp);

            _path.AddRange(nmp.corners);
            if (_path.Any() && Vector3.Distance(Unit.transform.position, _path.First()) <= .01f)
            {
                _path.RemoveAt(0);
            }
        }
    }
}