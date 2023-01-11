using System;
using Model.Items;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town
{
    public class ItemButtonUiController : MonoBehaviour
    {
        [SerializeField] private Image icon;
        private Item _item;
        public event Action<Item> OnClick;

        public Item Item
        {
            get => _item;
            set => SetItem(value);
        }

        private void SetItem(Item value)
        {
            _item = value;
            icon.sprite = value.icon;
        }

        public void Clicked()
        {
            OnClick?.Invoke(_item);
        }
    }
}