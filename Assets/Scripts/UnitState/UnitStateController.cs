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
using UnitState.Effects.Interfaces;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class UnitStateController : NetworkBehaviour
{
    public virtual UnitType UnitType => Unit.Value.type;
    public BattleBehavior BattleBehavior => Unit.Value.battleBehavior;
    public virtual Skills Skills => Unit.Value.Skills;
    public long MaxHp => Unit.Value.maxHp;
    public long HitPoints => Unit.Value.hitPoints;
    public string Name => Unit.Value.Name;
    public long OwnerId => Unit.Value.ownerId;
    public long Id => Unit.Value.Id;
    public float MovementSpeed => Unit.Value.movementSpeed + Unit.Value.movementSpeed * GetMovementSpeedModificator();
    public float AttackSpeed => Unit.Value.attackSpeed;
    public float AttackRange => Unit.Value.attackRange;
    public long AttackDamage => Unit.Value.damage;
    public long TeamId { get; private set; }
    public long Mana => Unit.Value.mana;
    public long MaxMana => Unit.Value.maxMana;

    public List<Effect> Effects { get; } = new();

    private ResourcesManager _resourcesManager;

    private void Start()
    {
        _resourcesManager = ResourcesManager.GetInstance();
    }

    private float GetMovementSpeedModificator()
    {
        return Effects.Select(e => e as IMovementSpeedPercentageChangeEffect).NotNull()
            .Sum(e => e.GetValue()) / 100f;
    }

    protected readonly NetworkVariable<Unit> Unit = new();

    public void Init(Unit unit, long? teamId = null)
    {
        Unit.Value = unit;
        BattleBehaviourManager.UpdateBattleBehaviour(this);
        TeamId = (teamId ?? OwnerId);
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
        Effects.Add(e);
        e.OnDestroy += () => Effects.Remove(e);
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
        var effectsValue = Effects.Select(e => e as IArmorChangeEffect).NotNull().Sum(e => e.GetAmount()) / 100;
        return armorValue + effectsValue;
    }

    private float GetCurrentMagicDamageReduction()
    {
        var armorValue = Unit.Value.magicResistance / 100;
        var effectsValue = Effects.Select(e => e as IMagicResistanceChangeEffect).NotNull().Sum(e => e.GetAmount());
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
        foreach (var e in Effects.Where(e => e is IPositiveEffect).ToList())
        {
            Destroy(e);
        }
    }

    public void ClearNegativeEffects()
    {
        foreach (var e in Effects.Where(e => e is INegativeEffect).ToList())
        {
            Destroy(e);
        }
    }

    public void ResetMana()
    {
        Unit.Value.mana = 0;
    }
}