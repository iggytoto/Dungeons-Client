using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using DefaultNamespace.UnitState;
using Model.Damage;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStateController : NetworkBehaviour
{
    public virtual Unit Unit => _unit.Value;

    private readonly NetworkVariable<Unit> _unit = new();
    private readonly List<Effect> _effects = new();

    public void Init(Unit unit)
    {
        _unit.Value = unit;
        BattleBehaviourManager.UpdateBattleBehaviour(this);
        gameObject.AddComponent<ManaRegenBehavior>();
    }

    public Unit ToUnit()
    {
        return _unit.Value;
    }

    public float GetCurrentSpeed()
    {
        return Unit?.movementSpeed ?? 4;
    }

    public double GetCurrentAttackRange()
    {
        return Unit?.attackRange ?? 1.5;
    }

    public void DoAttack(UnitStateController target)
    {
        if (target != null)
        {
            target.DoDamage(Damage.Physical(Unit.damage));
        }
    }

    public TEffect ApplyEffect<TEffect>() where TEffect : Effect
    {
        var e = gameObject.AddComponent<TEffect>();
        _effects.Add(e);
        e.OnDestroy += () => _effects.Remove(e);
        return e;
    }

    public void DoDamage(Damage unitDamage)
    {
        var damageAmount = unitDamage.Type switch
        {
            DamageType.Physical => unitDamage.Amount * GetCurrentDamageReduction(),
            DamageType.Magic => unitDamage.Amount * GetCurrentMagicDamageReduction(),
            _ => throw new ArgumentOutOfRangeException()
        };
        Unit.hitPoints -= (long)damageAmount;
        if (IsDead())
        {
            gameObject.GetComponentInChildren<Animator>().SetBool(AnimationConstants.IsDeadBoolean, true);
        }
    }

    private float GetCurrentDamageReduction()
    {
        var armorValue = Unit.armor / 100;
        var effectsValue = _effects.Select(e => e as IArmorChangeEffect).NotNull().Sum(e => e.GetAmount());
        return armorValue + effectsValue;
    }

    private float GetCurrentMagicDamageReduction()
    {
        var armorValue = Unit.magicResistance / 100;
        var effectsValue = _effects.Select(e => e as IMagicResistanceChangeEffect).NotNull().Sum(e => e.GetAmount());
        return armorValue + effectsValue;
    }

    public bool IsDead()
    {
        return Unit.hitPoints <= 0;
    }

    public float GetCurrentAttackSpeed()
    {
        return Unit?.attackSpeed ?? 1;
    }

    public long GetCurrentMana()
    {
        return Unit?.mana ?? 0;
    }

    public long GetMaxMana()
    {
        return Unit?.maxMana ?? 0;
    }

    public long GetMaxHp()
    {
        return Unit?.maxHp ?? 0;
    }
}