using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Services;
using UnityEngine;

public class BottomHorizontalBarrackUnitListUiController : MonoBehaviour
{
    [SerializeField] private GameObject unitButtonPrefab;
    [SerializeField] private GameObject content;
    private IBarrackService _barrackService;
    private readonly List<UnitButtonUiController> _unitButtonControllers = new();

    private void Start()
    {
        var gs = FindObjectOfType<GameService>();
        _barrackService = gs.BarrackService;
        _barrackService.AvailableUnits.CollectionChanged += BarrackAvailableUnitsOnCollectionChanged;
        AddToContentAsButton(_barrackService.AvailableUnits);
    }

    private void BarrackAvailableUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
        foreach (Unit unit in items)
        {
            var bc = _unitButtonControllers.FirstOrDefault(x => x.Unit.Id == unit.Id);
            if (bc == null) continue;
            Destroy(bc.gameObject);
            _unitButtonControllers.Remove(bc);
        }
    }

    private void AddToContentAsButton(IEnumerable items)
    {
        foreach (Unit unit in items)
        {
            if (_unitButtonControllers.Any(x => x.Unit.Id == unit.Id)) continue;
            var button = Instantiate(unitButtonPrefab, content.transform);
            var buttonController = button.GetComponent<UnitButtonUiController>();
            buttonController.Unit = unit;
            _unitButtonControllers.Add(buttonController);
        }
    }
}