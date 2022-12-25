public sealed class Gold : Currency
{
    public const long ID = 1;

    private Gold()
    {
        Name = "Gold";
        Id = ID;
    }

    public static Gold Of(decimal amount)
    {
        return new Gold
        {
            Amount = amount
        };
    }
}