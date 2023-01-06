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