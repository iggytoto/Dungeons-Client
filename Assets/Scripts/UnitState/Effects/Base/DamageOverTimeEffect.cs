using System;
using Model.Damage;
using UnityEngine;

namespace DefaultNamespace.UnitState
{
    /**
     * Damage over time is a generic effect that applied to a unit to do damage with given periodic.
     */
    public abstract class DamageOverTimeEffect : Effect
    {
        protected virtual float Interval { get; }
        protected virtual DamageType DamageType { get; }
        protected virtual DamageOverTimeType DotType { get; }
        protected virtual float DamageAmount { get; }
        private float _currentInterval;

        protected override void Update()
        {
            base.Update();
            _currentInterval -= Time.deltaTime;
            if (!(_currentInterval <= 0)) return;
            var damageAmount = DotType switch
            {
                DamageOverTimeType.FixedAmount => DamageAmount,
                DamageOverTimeType.PercentageOfMaxHp => UnitState.GetMaxHp() * DamageAmount / 100f,
                _ => throw new ArgumentOutOfRangeException()
            };
            var damage = DamageType switch
            {
                DamageType.Physical => Damage.Physical((long)damageAmount),
                DamageType.Magic => Damage.Magic((long)damageAmount),
                _ => throw new ArgumentOutOfRangeException()
            };
            UnitState.DoDamage(damage);
            _currentInterval = Interval;
        }

        protected enum DamageOverTimeType
        {
            FixedAmount,
            PercentageOfMaxHp
        }
    }
}