using System;
using Model.Units;
using Unity.Netcode;
using Random = System.Random;

[Serializable]
public class Unit : ModelBase, INetworkSerializable
{
    public long hitPoints;
    public long armor;
    public long magicResistance;
    public long damage;
    public long maxHp;
    public long mana;
    public long maxMana;
    public float attackSpeed;
    public float attackRange;
    public long ownerId;
    public float movementSpeed;
    public BattleBehavior battleBehavior;
    public UnitActivity activity;
    public UnitType type;
    public Equipment equip;

    public event Action OnDeath;


    public static Unit Random()
    {
        var rng = new Random();
        return new Unit
        {
            Id = rng.Next(0, 1000000),
            Name = rng.Next(0, 10000).ToString(),
            maxHp = rng.Next(100, 200),
            hitPoints = rng.Next(100, 200),
            attackSpeed = 1,
            armor = rng.Next(0, 100),
            magicResistance = rng.Next(0, 100),
            damage = rng.Next(20, 100),
            movementSpeed = 4,
            attackRange = 1.1f,
            activity = (UnitActivity)Enum.GetValues(typeof(UnitActivity)).GetValue(rng.Next(3))
        };
    }

    public static Unit Random(BattleBehavior bb)
    {
        var result = Random();
        result.battleBehavior = bb;
        return result;
    }

    public static Unit HumanWarrior()
    {
        return new Unit
        {
            hitPoints = 100,
            maxHp = 100,
            armor = 15,
            magicResistance = 0,
            activity = UnitActivity.Idle,
            battleBehavior = BattleBehavior.StraightAttack,
            attackRange = 2,
            attackSpeed = 1,
            movementSpeed = 6,
            damage = 50,
            maxMana = 100,
            type = UnitType.HumanWarrior,
            equip = new HumanWarriorEquipment()
        };
    }
    
    public static Unit HumanArcher()
    {
        return new Unit
        {
            hitPoints = 100,
            maxHp = 100,
            armor = 15,
            magicResistance = 0,
            activity = UnitActivity.Idle,
            battleBehavior = BattleBehavior.StraightAttack,
            attackRange = 2,
            attackSpeed = 1,
            movementSpeed = 6,
            damage = 50,
            maxMana = 100,
            type = UnitType.HumanWarrior,
            equip = new HumanWarriorEquipment()
        };
    }


    [Serializable]
    public enum UnitActivity
    {
        Idle,
        Training,
        Dead
    }

    public void DoDamage(long unitDamage)
    {
        hitPoints -= (long)(unitDamage * Math.Log(armor, 1.1) / 100);
        if (hitPoints <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Id);
        serializer.SerializeValue(ref Name);
        serializer.SerializeValue(ref hitPoints);
        serializer.SerializeValue(ref armor);
        serializer.SerializeValue(ref magicResistance);
        serializer.SerializeValue(ref damage);
        serializer.SerializeValue(ref maxHp);
        serializer.SerializeValue(ref mana);
        serializer.SerializeValue(ref maxMana);
        serializer.SerializeValue(ref attackSpeed);
        serializer.SerializeValue(ref attackRange);
        serializer.SerializeValue(ref ownerId);
        serializer.SerializeValue(ref movementSpeed);
        serializer.SerializeValue(ref battleBehavior);
        serializer.SerializeValue(ref activity);
        serializer.SerializeValue(ref type);
    }
}