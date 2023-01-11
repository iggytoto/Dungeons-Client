namespace Model.Items
{
    public class Item : ModelBase
    {
        public long UnitId;
        public long UserId;
        public ItemType ItemType;
        public ItemRarity Rarity;
    }
}