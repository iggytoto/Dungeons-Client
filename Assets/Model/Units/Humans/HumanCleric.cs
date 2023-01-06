namespace Model.Units.Humans
{
    public class HumanCleric : Unit
    {
        public HumanCleric()
        {
            hitPoints = 25;
            maxHp = 25;
            armor = 0;
            magicResistance = 10;
            activity = UnitActivity.Idle;
            battleBehavior = BattleBehavior.StraightAttack;
            attackRange = 10;
            attackSpeed = 1;
            movementSpeed = 4;
            damage = 25;
            maxMana = 150;
            type = UnitType.HumanCleric;
            equip = new HumanClericEquipment();
        }
    }
}