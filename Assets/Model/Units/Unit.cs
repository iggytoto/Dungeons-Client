using System;
using Model.Units;

[Serializable]
public class Unit : ModelBase
{
    public long HitPoints { get; set; }
    public long Armor { get; set; }
    public long MagicResistance { get; set; }
    public long Damage { get; set; }
    public long MaxHp { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange => 2;
    public long OwnerId { get; set; }
    public long TrainingExperience { get; set; }
    public float MovementSpeed => 4;
    public BattleBehaviour BattleBehaviour { get; set; }
    public UnitActivity Activity { get; set; }
    public UnitType Type => UnitType.Dummy;

    public event Action OnDeath;

    public static Unit Random()
    {
        var rng = new Random();
        return new Unit
        {
            Id = rng.Next(0, 1000000),
            Name = rng.Next(0, 10000).ToString(),
            HitPoints = rng.Next(100, 200),
            MaxHp = rng.Next(100, 200),
            AttackSpeed = 1,
            Armor = rng.Next(0, 100),
            MagicResistance = rng.Next(0, 100),
            Damage = rng.Next(20, 100),
            Activity = (UnitActivity)Enum.GetValues(typeof(UnitActivity)).GetValue(rng.Next(3))
        };
    }

    public static Unit Random(BattleBehaviour bb)
    {
        var result = Random();
        result.BattleBehaviour = bb;
        return result;
    }

    public bool IsDead()
    {
        return HitPoints <= 0;
    }

    public enum UnitActivity
    {
        Idle,
        Training,
        Dead
    }

    public void DoDamage(long unitDamage)
    {
        HitPoints -= (long)(unitDamage * Math.Log(Armor, 1.1) / 100);
        if (HitPoints <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}