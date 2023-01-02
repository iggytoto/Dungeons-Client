using System;

[Serializable]
public class UnitForSale : Unit
{
    public long GoldPrice { get; protected set; }

    public static UnitForSale Of(Unit unit, long goldPrice)
    {
        return new UnitForSale
        {
            Id = unit.Id,
            armor = unit.armor,
            damage = unit.damage,
            Icon = unit.Icon,
            Name = unit.Name,
            hitPoints = unit.hitPoints,
            magicResistance = unit.magicResistance,
            ownerId = unit.ownerId,
            GoldPrice = goldPrice
        };
    }
}