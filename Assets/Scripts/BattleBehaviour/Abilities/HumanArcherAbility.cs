using System;
using System.Linq;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using DefaultNamespace.UnitState;
using Model.Damage;
using Model.Units;

namespace BattleBehaviour.Abilities
{
    public class HumanArcherAbility : UnitTaskBase
    {
        private readonly HumanArcherEquipment _equipment;

        public HumanArcherAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
            _equipment = (HumanArcherEquipment)unitStateStateController.Equipment;
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
                var attackClipInfo = Animator.GetCurrentAnimatorClipInfo(1)[0];
                var animationTime = attackClipInfo.clip.averageDuration;
                Animator.SetFloat(AnimationConstants.AttackMotionTimeFloat,
                    animationTime * UnitState.AttackSpeed / animationTime);
                foreach (var target in targets)
                {
                    var additionalDamageAndEffect = CalculateDamageAndEffect();
                    target.DoDamage(Damage.Physical(UnitState.AttackDamage +
                                                    additionalDamageAndEffect.additionalDamage));
                    if (_equipment.fireArrows)
                    {
                        target.ApplyEffect<FireArrowsBurningEffect>();
                    }

                    if (_equipment.poisonArrows)
                    {
                        target.ApplyEffect<PoisonArrowsEffect>();
                    }

                    if (!(additionalDamageAndEffect.msIncreaseDuration > 0)) continue;
                    var e = UnitState.ApplyEffect<HumanArcherAbilityProtectionEffect>();
                    e.Init(additionalDamageAndEffect.msIncreaseDuration,
                        additionalDamageAndEffect.msIncreaseAmount);
                }

                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }

        private AdditionalDamageAndEffect CalculateDamageAndEffect()
        {
            return (_equipment.midRangePoints, _equipment.longRangePoints) switch
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
                additionalDamage = d;
                msIncreaseAmount = ms;
                msIncreaseDuration = du;
            }

            public long additionalDamage;
            public long msIncreaseAmount;
            public float msIncreaseDuration;
        }
    }
}