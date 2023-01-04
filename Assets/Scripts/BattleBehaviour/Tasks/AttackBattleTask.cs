using DefaultNamespace.Animation;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class AttackBattleTask : UnitTaskBase
    {
        private float _attackCooldown;

        public AttackBattleTask(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            _attackCooldown -= Time.deltaTime;
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

            if (_attackCooldown <= 0)
            {
                Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
                Animator.SetTrigger(AnimationConstants.AttackTrigger);
                var attackClipInfo = Animator.GetCurrentAnimatorClipInfo(1)[0];
                var animationTime = attackClipInfo.clip.averageDuration;
                Animator.SetFloat(AnimationConstants.AttackMotionTimeFloat,
                    animationTime * UnitState.GetCurrentAttackSpeed() / animationTime);
                UnitState.DoAttack(target);
                _attackCooldown = 1 / UnitState.GetCurrentAttackSpeed();
                if (target.IsDead())
                {
                    ClearTarget();
                }

                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }
    }
}