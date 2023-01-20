using System.Collections.Specialized;
using DefaultNamespace.Ui.Scenes.Town;
using Model.Items;
using Services;
using UnityEngine;

public class TownSceneUiController : MonoBehaviour
{
    [SerializeField] private UnitListPanelUiController bottomHorizontalBarrackUnitListUiController;
    [SerializeField] private ItemsPanelUiController itemsPanelUiController;
    [SerializeField] private SelectedUnitPanelUiController selectedUnitPanelUiController;
    [SerializeField] private TrainingMatchPanelUiController trainingMatchPanelUiController;
    [SerializeField] private BuyUnitPopupUiController buyUnitPopupUiController;
    private IBarrackService _barrackService;

    private void Start()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;

        _barrackService.AvailableItems.CollectionChanged += (_, items) => OnBarrackAvailableItemsChanged(items);
        _barrackService.AvailableUnits.CollectionChanged += (_, units) => OnBarrackAvailableUnitsChanged(units);

        itemsPanelUiController.OnItemClicked += EquipItemOnSelectedUnit;
        selectedUnitPanelUiController.OnItemClicked += UnEquipItemFromSelectedUnit;
        bottomHorizontalBarrackUnitListUiController.OnUnitClicked += OnBarrackUnitClicked;
        buyUnitPopupUiController.UnitPurchased += OnUnitPurchased;

        foreach (var unit in _barrackService.AvailableUnits)
        {
            OnBarrackUnitAdded(unit);
        }

        foreach (var item in _barrackService.AvailableItems)
        {
            OnBarrackItemAdded(item);
        }
    }

    private void OnUnitPurchased(Unit obj)
    {
        OnBarrackUnitAdded(obj);
    }

    private void OnBarrackUnitClicked(Unit u)
    {
        trainingMatchPanelUiController.AddToRoster(u);
        selectedUnitPanelUiController.Unit = u;
    }

    private void OnBarrackAvailableUnitsChanged(NotifyCollectionChangedEventArgs units)
    {
        if (units.NewItems != null)
        {
            foreach (Unit item in units.NewItems)
            {
                OnBarrackUnitAdded(item);
            }
        }

        if (units.OldItems == null) return;
        {
            foreach (Unit item in units.OldItems)
            {
                OnBarrackUnitRemoved(item);
            }
        }
    }

    private void OnBarrackUnitAdded(Unit item)
    {
        bottomHorizontalBarrackUnitListUiController.AddUnit(item);
    }

    private void OnBarrackUnitRemoved(Unit item)
    {
        bottomHorizontalBarrackUnitListUiController.RemoveUnit(item);
    }

    private void OnBarrackAvailableItemsChanged(NotifyCollectionChangedEventArgs items)
    {
        if (items.NewItems != null)
        {
            foreach (Item item in items.NewItems)
            {
                OnBarrackItemAdded(item);
            }
        }

        if (items.OldItems == null) return;
        {
            foreach (Item item in items.OldItems)
            {
                OnBarrackItemRemoved(item);
            }
        }
    }

    private void OnBarrackItemRemoved(Item item)
    {
        itemsPanelUiController.RemoveItem(item);
    }

    private void OnBarrackItemAdded(Item item)
    {
        itemsPanelUiController.AddItem(item);
    }

    private void UnEquipItemFromSelectedUnit(Item i)
    {
        if (selectedUnitPanelUiController.Unit == null) return;
        if (!selectedUnitPanelUiController.Unit.UnEquipItem(i)) return;
        _barrackService.UnEquipItem(i);
        itemsPanelUiController.AddItem(i);
        selectedUnitPanelUiController.RemoveItem(i);
    }

    private void EquipItemOnSelectedUnit(Item i)
    {
        if (selectedUnitPanelUiController.Unit == null) return;
        if (!selectedUnitPanelUiController.Unit.EquipItem(i)) return;
        _barrackService.EquipItem(i, selectedUnitPanelUiController.Unit);
        itemsPanelUiController.RemoveItem(i);
        selectedUnitPanelUiController.AddItem(i);
    }
}