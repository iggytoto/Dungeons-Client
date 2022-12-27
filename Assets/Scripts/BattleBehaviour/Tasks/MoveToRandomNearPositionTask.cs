using DefaultNamespace.Animation;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class MoveToRandomNearPositionTask : UnitTaskBase
    {
        private bool _isWaiting = true;
        private float _waitingCounter;
        private readonly float _waitInterval;
        private Vector3 _destination;

        public MoveToRandomNearPositionTask(
            UnitStateController unitStateController,
            float waitInterval) : base(unitStateController)
        {
            _waitInterval = waitInterval;
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            if (_isWaiting)
            {
                _waitingCounter += Time.deltaTime;
                if (_waitingCounter >= _waitInterval)
                {
                    _destination = GetRandomPositionNearBy();
                    _isWaiting = false;
                    Animator.SetBool(AnimationConstants.IsRunningBoolean, true);
                }
            }
            else
            {
                if (Vector3.Distance(Unit.transform.position, _destination) < .01f)
                {
                    Unit.transform.position = _destination;
                    _waitingCounter = 0;
                    _isWaiting = true;
                    Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
                }
                else
                {
                    Unit.gameObject.transform.position =
                        Vector3.MoveTowards(Unit.transform.position, _destination,
                            Unit.GetCurrentSpeed() * Time.deltaTime);
                    Unit.gameObject.transform.LookAt(_destination);
                }
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }

        private Vector3 GetRandomPositionNearBy()
        {
            var random = Random.insideUnitCircle * 5;
            return Unit.transform.position + new Vector3(random.x, 0, random.y);
        }
    }
}