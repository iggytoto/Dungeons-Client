using UnitState.Effects.Interfaces;

namespace DefaultNamespace.UnitState
{
    public class HumanArcherAbilityProtectionEffect : Effect, IMovementSpeedPercentageChangeEffect, IPositiveEffect
    {
        private float _duration;
        private long _msIncrease;
        protected override float EffectDuration => _duration;
        public override long Id => 2002;

        public void Init(float duration, long msIncrease)
        {
            _duration = duration;
            _msIncrease = msIncrease;
        }

        public long GetValue()
        {
            return _msIncrease;
        }
    }
}