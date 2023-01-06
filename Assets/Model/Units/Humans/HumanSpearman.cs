namespace Model.Units.Humans
{
    public class HumanSpearman : Unit
    {
        public HumanSpearman()
        {
            hitPoints = 50;
            maxHp = 50;
            armor = 10;
            magicResistance = 0;
            activity = UnitActivity.Idle;
            battleBehavior = BattleBehavior.StraightAttack;
            attackRange = 3;
            attackSpeed = 2;
            movementSpeed = 7;
            damage = 40;
            maxMana = 75;
            type = UnitType.HumanSpearman;
            equip = new HumanSpearmanEquipment();
        }
    }
}