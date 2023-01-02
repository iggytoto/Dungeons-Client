using DefaultNamespace.Animation;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class AttackBattleTask : UnitTaskBase
    {
        private float _attackCooldown;

        public AttackBattleTask(UnitStateController unitStateController) : base(unitStateController)
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

            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown <= 0)
            {
                Animator.SetBool(AnimationConstants.IsRunningBoolean, false);
                Animator.SetTrigger(AnimationConstants.AttackTrigger);
                var attackClipInfo = Animator.GetCurrentAnimatorClipInfo(1)[0];
                var animationTime = attackClipInfo.clip.averageDuration;
                Animator.SetFloat(AnimationConstants.AttackMotionTimeFloat,
                    animationTime * Unit.GetCurrentAttackSpeed() / animationTime);
                Unit.DoAttack(target);
                _attackCooldown = 1 / Unit.GetCurrentAttackSpeed();
                if (target.IsDead())
                {
                    ClearTarget();
                }
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }
    }
}