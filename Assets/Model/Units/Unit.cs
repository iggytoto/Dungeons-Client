using System;

[Serializable]
public class Unit : ModelBase
{
    public long HitPoints { get; set; }
    public long Armor { get; set; }
    public long MagicResistance { get; set; }
    public long Damage { get; set; }

    public long OwnerId { get; set; }
    public long TrainingExperience { get; set; }
    public UnitActivity Activity { get; set; }

    public static Unit Random()
    {
        var rng = new Random();
        return new Unit
        {
            Id = rng.Next(0, 1000000),
            Name = rng.Next(0, 10000).ToString(),
            HitPoints = rng.Next(100, 200),
            Armor = rng.Next(0, 100),
            MagicResistance = rng.Next(0, 100),
            Damage = rng.Next(20, 100),
            Activity = (UnitActivity)Enum.GetValues(typeof(UnitActivity)).GetValue(rng.Next(3))
        };
    }

    public enum UnitActivity
    {
        Idle,
        Training,
        Dead
    }
}