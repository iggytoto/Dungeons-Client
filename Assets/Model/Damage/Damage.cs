namespace Model.Damage
{
    public class Damage
    {
        public readonly long Amount;
        public readonly DamageType Type;

        protected Damage(long amount, DamageType t)
        {
            Amount = amount;
            Type = t;
        }

        public static Damage Physical(long amount)
        {
            return new Damage(amount, DamageType.Physical);
        }

        public static Damage Magic(long amount)
        {
            return new Damage(amount, DamageType.Magic);
        }
    }
}