using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Model.Items;
using Services;
using UnityEngine;
using UnityEngine.Events;

public class ItemsPanelUiController : MonoBehaviour
{
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemsContent;
    [SerializeField] private UnityEvent<Item> onItemClicked;
    private IBarrackService _barrackService;
    private readonly List<ItemButtonUiController> _itemButtonControllers = new();


    private void Start()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
        _barrackService.AvailableItems.CollectionChanged += OnBarrackAvailableItemsOnCollectionChanged;
        AddToContentAsButton(_barrackService.AvailableItems);
    }

    private void AddToContentAsButton(IEnumerable items)
    {
        foreach (Item item in items)
        {
            if (_itemButtonControllers.Any(x => x.Item.Id == item.Id)) continue;
            var button = Instantiate(itemButtonPrefab, itemsContent.transform);
            var buttonController = button.GetComponent<ItemButtonUiController>();
            buttonController.OnClick += OnItemClickedInternal;
            buttonController.Item = item;
            _itemButtonControllers.Add(buttonController);
        }
    }

    private void OnBarrackAvailableItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            AddToContentAsButton(e.NewItems);
        }

        if (e.OldItems != null)
        {
            RemoveFromContent(e.OldItems);
        }
    }

    private void RemoveFromContent(IList items)
    {
        foreach (Item item in items)
        {
            var bc = _itemButtonControllers.FirstOrDefault(x => x.Item.Id == item.Id);
            if (bc == null) continue;
            Destroy(bc.gameObject);
            _itemButtonControllers.Remove(bc);
        }
    }

    private void OnItemClickedInternal(object sender, Item item)
    {
        onItemClicked.Invoke(item);
    }
}