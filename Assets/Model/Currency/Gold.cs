using UnityEngine;

public sealed class Gold : Currency
{
    public const long ID = 1;
    public override long Id => ID;
    public override Sprite Icon => null;
    public override string Name => "Gold";

    private Gold()
    {
    }

    public static Gold Of(decimal amount)
    {
        return new Gold
        {
            Amount = amount
        };
    }
}