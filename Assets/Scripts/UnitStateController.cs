using DefaultNamespace;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using Unity.Netcode;
using UnityEngine;

public class UnitStateController : NetworkBehaviour
{
    public virtual Unit Unit => _unit.Value;

    private readonly NetworkVariable<Unit> _unit = new();

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
            target.DoDamage(Unit.damage);
        }
    }

    private void DoDamage(long unitDamage)
    {
        Unit.hitPoints -= unitDamage * (100 - Unit.armor) / 100;
        if (IsDead())
        {
            gameObject.GetComponentInChildren<Animator>().SetBool(AnimationConstants.IsDeadBoolean, true);
        }
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
}