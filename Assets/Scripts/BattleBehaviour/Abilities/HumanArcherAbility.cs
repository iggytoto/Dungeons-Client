using System;
using System.Collections;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using DefaultNamespace.UnitState;
using Model.Damage;
using Model.Units;
using UnityEngine;

namespace BattleBehaviour.Abilities
{
    public class HumanArcherAbility : UnitTaskBase
    {
        private HumanArcherSkills _skills;

        public HumanArcherAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            var targets = GetAllLiveEnemies().OrderBy(e => Guid.NewGuid()).Take(3).ToArray();

            if (targets.Any() && UnitState.Mana >= UnitState.MaxMana)
            {
                UnitState.Mana = 0;
                Animator.SetTrigger(AnimationConstants.AttackTrigger);
                var animationTime = GetAnimationTime();
                Animator.SetFloat(AnimationConstants.AttackMotionTimeFloat,
                    animationTime * UnitState.AttackSpeed / animationTime);
                foreach (var target in targets)
                {
                    var additionalDamageAndEffect = CalculateDamageAndEffect();
                    UnitState.StartCoroutine(DelayedAttack(target, animationTime, additionalDamageAndEffect));
                    if (!(additionalDamageAndEffect.MSIncreaseDuration > 0)) continue;
                    var e = UnitState.ApplyEffect<HumanArcherAbilityProtectionEffect>();
                    e.Init(additionalDamageAndEffect.MSIncreaseDuration,
                        additionalDamageAndEffect.MSIncreaseAmount);
                }

                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }

        private AdditionalDamageAndEffect CalculateDamageAndEffect()
        {
            return (GetEquipment().midRangePoints, GetEquipment().longRangePoints) switch
            {
                (0, 1) => new AdditionalDamageAndEffect(25, 0, 0),
                (0, 2) => new AdditionalDamageAndEffect(50, 0, 0),
                (0, 3) => new AdditionalDamageAndEffect(75, 0, 0),
                (0, 4) => new AdditionalDamageAndEffect(100, 0, 0),
                (1, 0) => new AdditionalDamageAndEffect(0, 25, 2),
                (1, 1) => new AdditionalDamageAndEffect(25, 25, 2),
                (1, 2) => new AdditionalDamageAndEffect(30, 25, 1),
                (1, 3) => new AdditionalDamageAndEffect(35, 20, 1),
                (1, 4) => new AdditionalDamageAndEffect(40, 15, 1),
                (2, 0) => new AdditionalDamageAndEffect(0, 30, 3),
                (2, 1) => new AdditionalDamageAndEffect(15, 30, 3),
                (2, 2) => new AdditionalDamageAndEffect(20, 30, 3),
                (2, 3) => new AdditionalDamageAndEffect(30, 30, 1),
                (2, 4) => new AdditionalDamageAndEffect(35, 20, 1),
                (3, 0) => new AdditionalDamageAndEffect(0, 35, 3),
                (3, 1) => new AdditionalDamageAndEffect(10, 35, 3),
                (3, 2) => new AdditionalDamageAndEffect(15, 30, 3),
                (3, 3) => new AdditionalDamageAndEffect(20, 30, 2),
                (4, 0) => new AdditionalDamageAndEffect(0, 50, 4),
                (4, 1) => new AdditionalDamageAndEffect(10, 50, 4),
                (4, 2) => new AdditionalDamageAndEffect(15, 50, 3),
                _ => new AdditionalDamageAndEffect()
            };
        }

        private class AdditionalDamageAndEffect
        {
            public AdditionalDamageAndEffect()
            {
            }

            public AdditionalDamageAndEffect(long d, long ms, float du)
            {
                AdditionalDamage = d;
                MSIncreaseAmount = ms;
                MSIncreaseDuration = du;
            }

            public readonly long AdditionalDamage;
            public readonly long MSIncreaseAmount;
            public readonly float MSIncreaseDuration;
        }

        private HumanArcherSkills GetEquipment()
        {
            return _skills ??= (HumanArcherSkills)UnitState.Skills;
        }

        private IEnumerator DelayedAttack(UnitStateController target, float animationTime,
            AdditionalDamageAndEffect additionalDamageAndEffect)
        {
            yield return new WaitForSeconds(animationTime);
            UnitState.DoAttack(
                target,
                true,
                Damage.Physical(UnitState.AttackDamage +
                                additionalDamageAndEffect.AdditionalDamage),
                (t) =>
                {
                    if (GetEquipment().fireArrows)
                    {
                        t.ApplyEffect<FireArrowsBurningEffect>();
                    }

                    if (GetEquipment().poisonArrows)
                    {
                        t.ApplyEffect<PoisonArrowsEffect>();
                    }
                });
        }
    }
}