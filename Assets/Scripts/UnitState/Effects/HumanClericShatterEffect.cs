using UnitState.Effects.Interfaces;

namespace DefaultNamespace.UnitState
{
    public class HumanClericShatterEffect : Effect, IArmorChangeEffect, IPositiveEffect, INegativeEffect
    {
        private float _duration;
        private long _armorIncrease;
        protected override float EffectDuration => _duration;
        public override long Id => 3000;

        public void Init(float duration, long armorIncrease)
        {
            _duration = duration;
            _armorIncrease = armorIncrease;
        }

        public long GetAmount()
        {
            return _armorIncrease;
        }
    }
}