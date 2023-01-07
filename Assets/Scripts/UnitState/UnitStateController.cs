using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using DefaultNamespace.Projectiles;
using DefaultNamespace.UnitState;
using Model.Damage;
using Model.Units;
using Services;
using UnitState.Effects.Interfaces;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStateController : NetworkBehaviour
{
    public virtual UnitType UnitType => Unit.Value.type;
    public BattleBehavior BattleBehavior => Unit.Value.battleBehavior;
    public virtual Equipment Equipment => Unit.Value.equip;
    public float MaxHp => Unit.Value.maxHp;
    public float HitPoints => Unit.Value.hitPoints;
    public string Name => Unit.Value.Name;
    public long OwnerId => Unit.Value.ownerId;
    public long Id => Unit.Value.Id;
    public float MovementSpeed => Unit.Value.movementSpeed + Unit.Value.movementSpeed * GetMovementSpeedModificator();
    public float AttackSpeed => Unit.Value.attackSpeed;
    public float AttackRange => Unit.Value.attackRange;
    public long AttackDamage => Unit.Value.damage;

    public long Mana
    {
        get => Unit.Value.mana;
        set => Unit.Value.mana = value;
    }

    public long MaxMana => Unit.Value.maxMana;

    private ResourcesManager _resourcesManager;

    private void Start()
    {
        _resourcesManager = FindObjectOfType<GameService>().ResourcesManager;
    }

    private float GetMovementSpeedModificator()
    {
        return _effects.Select(e => e as IMovementSpeedPercentageChangeEffect).NotNull()
            .Sum(e => e.GetValue()) / 100f;
    }

    protected readonly NetworkVariable<Unit> Unit = new();
    private readonly List<Effect> _effects = new();

    public void Init(Unit unit)
    {
        Unit.Value = unit;
        BattleBehaviourManager.UpdateBattleBehaviour(this);
    }

    public Unit ToUnit()
    {
        return Unit.Value;
    }

    public void DoAttack(
        UnitStateController target,
        bool doNotRegenMana = false,
        Damage damageOverride = null,
        Action<UnitStateController> onProjectileHitHandler = null)
    {
        if (target == null) return;
        var projectilePrefab = _resourcesManager.LoadProjectileForUnitType(Unit.Value.type);
        var damage = damageOverride ?? Damage.Physical(AttackDamage);
        if (projectilePrefab == null)
        {
            target.DoDamage(damage);
        }
        else
        {
            var projectileObject = Instantiate(projectilePrefab);
            var pc = projectileObject.GetComponent<ProjectileController>();
            pc.Init(this, target, damage, onProjectileHitHandler);
        }

        if (!doNotRegenMana)
        {
            Unit.Value.mana += 5;
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
            DamageType.Physical => unitDamage.Amount * (1f - GetCurrentDamageReduction()),
            DamageType.Magic => unitDamage.Amount * (1f - GetCurrentMagicDamageReduction()),
            _ => throw new ArgumentOutOfRangeException()
        };
        Unit.Value.hitPoints -= (long)damageAmount;
        if (IsDead())
        {
            gameObject.GetComponentInChildren<Animator>().SetBool(AnimationConstants.IsDeadBoolean, true);
        }

        Debug.Log($"Unit with id:{Unit.Value.Id} suffered {damageAmount} damage");
    }

    private float GetCurrentDamageReduction()
    {
        var armorValue = Unit.Value.armor / 100;
        var effectsValue = _effects.Select(e => e as IArmorChangeEffect).NotNull().Sum(e => e.GetAmount()) / 100;
        return armorValue + effectsValue;
    }

    private float GetCurrentMagicDamageReduction()
    {
        var armorValue = Unit.Value.magicResistance / 100;
        var effectsValue = _effects.Select(e => e as IMagicResistanceChangeEffect).NotNull().Sum(e => e.GetAmount());
        return armorValue + effectsValue;
    }

    public bool IsDead()
    {
        return Unit.Value.hitPoints <= 0;
    }

    public void Heal(long value)
    {
        Unit.Value.hitPoints += value;
        Math.Clamp(Unit.Value.hitPoints, 0, Unit.Value.maxHp);
    }

    public void ClearPositiveEffects()
    {
        foreach (var e in _effects.Where(e => e is IPositiveEffect).ToList())
        {
            Destroy(e);
        }
    }

    public void ClearNegativeEffects()
    {
        foreach (var e in _effects.Where(e => e is INegativeEffect).ToList())
        {
            Destroy(e);
        }
    }
}