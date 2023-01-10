using System;
using Model.Items;
using UnityEngine;

namespace DefaultNamespace.Ui.Scenes.Town
{
    public class ItemButtonUiController : MonoBehaviour
    {
        private Item _item;
        public event EventHandler<Item> OnClick;

        public Item Item
        {
            get => _item;
            set => SetItem(value);
        }

        private void SetItem(Item value)
        {
            _item = value;
        }
    }
}