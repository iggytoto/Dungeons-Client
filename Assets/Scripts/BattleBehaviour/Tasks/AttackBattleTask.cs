using System.Collections;
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
            if (target.IsDead())
            {
                ClearTarget();
                target = null;
            }

            if (target == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            if (!IsWithinAttackRange(target))
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
                    animationTime * UnitState.AttackSpeed / animationTime);
                UnitState.gameObject.transform.LookAt(target.transform.position);
                UnitState.StartCoroutine(DelayedAttack(target, animationTime));
                _attackCooldown = 1 / UnitState.AttackSpeed;
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }

        private IEnumerator DelayedAttack(UnitStateController target, float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            UnitState.DoAttack(target);
            if (target.IsDead())
            {
                ClearTarget();
            }
        }
    }
}