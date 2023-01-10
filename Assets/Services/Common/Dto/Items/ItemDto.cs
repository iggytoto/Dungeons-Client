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

        public Item ToDomain()
        {
            return new Item
            {
                id = id,
                unitId = unitId,
                userId = userId,
                ItemType = itemType,
                icon = ResourcesManager.GetInstance().GetIconForItem(itemType)
            };
        }
    }
}