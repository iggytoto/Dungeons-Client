using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Model.Items;
using UnityEngine;

public class ItemsPanelUiController : MonoBehaviour
{
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemsContent;
    public event Action<Item> OnItemClicked;

    private readonly List<ItemButtonUiController> _itemButtonControllers = new();

    public void RemoveItem(Item i)
    {
        var bc = _itemButtonControllers.FirstOrDefault(x => x.Item.Id == i.Id);
        if (bc == null) return;
        Destroy(bc.gameObject);
        _itemButtonControllers.Remove(bc);
    }

    public void AddItem(Item item)
    {
        if (_itemButtonControllers.Any(x => x.Item.Id == item.Id)) return;
        var button = Instantiate(itemButtonPrefab, itemsContent.transform);
        var buttonController = button.GetComponent<ItemButtonUiController>();
        buttonController.OnClick += (i) => OnItemClicked?.Invoke(i);
        buttonController.Item = item;
        _itemButtonControllers.Add(buttonController);
    }
}