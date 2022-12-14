using System;
using System.Collections.Generic;
using System.Linq;
using Model.Items;
using Model.Units;
using Unity.Netcode;

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
    public Skills Skills;
    public readonly List<Item> Items = new();

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

    public bool EquipItem(Item item)
    {
        if (Items.Any(i => i.Id == item.Id))
        {
            return false;
        }

        Items.Add(item);
        return true;
    }

    public bool UnEquipItem(Item item)
    {
        return Items.Remove(item);
    }
}