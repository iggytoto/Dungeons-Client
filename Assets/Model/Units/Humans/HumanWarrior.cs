namespace Model.Units.Humans
{
    public class HumanWarrior : Unit
    {
        public HumanWarrior()
        {
            hitPoints = 100;
            maxHp = 100;
            armor = 15;
            magicResistance = 0;
            activity = UnitActivity.Idle;
            battleBehavior = BattleBehavior.StraightAttack;
            attackRange = 2;
            attackSpeed = 1;
            movementSpeed = 6;
            damage = 50;
            maxMana = 100;
            type = UnitType.HumanWarrior;
            equip = new HumanWarriorEquipment();
        }
    }
}