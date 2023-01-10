using UnityEngine;

namespace Model.Items
{
    public class Item : ModelBase
    {
        public long id;
        public long unitId;
        public long userId;
        public ItemType ItemType;
        public Sprite icon;
    }
}