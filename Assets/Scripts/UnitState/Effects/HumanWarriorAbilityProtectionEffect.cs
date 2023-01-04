namespace DefaultNamespace.UnitState
{
    public class HumanWarriorAbilityProtectionEffect : Effect, IArmorChangeEffect, IMagicResistanceChangeEffect
    {
        private long _mrIncrease;
        private float _duration;
        private long _armorIncrease;
        protected override float EffectDuration => _duration;
        public override long Id => 2000;

        public void Init(float duration, long armorIncrease, long magicResistanceIncrease)
        {
            _duration = duration;
            _armorIncrease = armorIncrease;
            _mrIncrease = magicResistanceIncrease;
        }

        long IArmorChangeEffect.GetAmount()
        {
            return _armorIncrease;
        }

        long IMagicResistanceChangeEffect.GetAmount()
        {
            return _mrIncrease;
        }
    }
}