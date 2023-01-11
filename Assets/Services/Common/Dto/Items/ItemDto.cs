using System;
using DefaultNamespace;
using Model.Items;
using Services.Dto;

namespace Services.Common.Dto.Items
{
    [Serializable]
    public class ItemDto : ResponseBaseDto
    {
        public long id;
        public long userId;
        public long unitId;
        public ItemType itemType;
        public string name;
        public ItemRarity rarity;

        public Item ToDomain()
        {
            return new Item
            {
                Id = id,
                UnitId = unitId,
                UserId = userId,
                ItemType = itemType,
                Rarity = rarity,
                Name = name,
                Icon = ResourcesManager.GetInstance().GetIconForItem(itemType)
            };
        }
    }
}