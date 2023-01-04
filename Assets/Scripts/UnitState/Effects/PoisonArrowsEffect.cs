using Model.Damage;

namespace DefaultNamespace.UnitState
{
    public class PoisonArrowsEffect : DamageOverTimeEffect
    {
        protected override float Interval => 1;
        protected override DamageType DamageType => DamageType.Magic;
        protected override DamageOverTimeType DotType => DamageOverTimeType.PercentageOfMaxHp;
        protected override float DamageAmount => 1f;
        protected override float EffectDuration => 10;
        public override long Id => 1001;
    }
}