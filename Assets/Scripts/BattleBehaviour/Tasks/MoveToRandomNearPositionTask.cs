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
            UnitStateController unitStateStateController,
            float waitInterval) : base(unitStateStateController)
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
                if (Vector3.Distance(UnitState.transform.position, _destination) < .01f)
                {
                    UnitState.transform.position = _destination;
                    _waitingCounter = 0;
                    _isWaiting = true;
                    Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
                }
                else
                {
                    UnitState.gameObject.transform.position =
                        Vector3.MoveTowards(UnitState.transform.position, _destination,
                            UnitState.GetCurrentSpeed() * Time.deltaTime);
                    UnitState.gameObject.transform.LookAt(_destination);
                }
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }

        private Vector3 GetRandomPositionNearBy()
        {
            var random = Random.insideUnitCircle * 5;
            return UnitState.transform.position + new Vector3(random.x, 0, random.y);
        }
    }
}