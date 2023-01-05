using Model.Damage;
using UnitState.Effects.Interfaces;

namespace DefaultNamespace.UnitState
{
    /**
     * Fire arrows burning effect its magic DOT effect  that reduces incoming healing.
     */
    public class FireArrowsBurningEffect : DamageOverTimeEffect, IHealingAmountChangeEffect
    {
        protected override float Interval => 1;
        protected override DamageType DamageType => DamageType.Magic;
        protected override DamageOverTimeType DotType => DamageOverTimeType.PercentageOfMaxHp;
        protected override float DamageAmount => .5f;
        protected override float EffectDuration => 10;
        public override long Id => 1000;

        private const long HealingReductionValuePercentage = 33;

        public long GetAmount()
        {
            return -HealingReductionValuePercentage;
        }
    }
}