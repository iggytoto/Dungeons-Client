using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DefaultNamespace;
using Services;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalUnitListController : MonoBehaviour
{
    [SerializeField] public SourceType source = SourceType.Tavern;
    [SerializeField] public GameObject unitListProvider;
    public GameObject content;
    public GameObject unitButtonPrefab;

    [SerializeField] public UnityEvent<Unit> unitButtonClicked = new();

    private readonly List<HorizontalUnitListButtonController> _contentButtonControllers = new();


    private void Start()
    {
        switch (source)
        {
            case SourceType.Tavern:
                ProcessSource(FindObjectOfType<GameService>().TavernService.Units);
                break;
            case SourceType.Barrack:
                ProcessSource(FindObjectOfType<GameService>().BarrackService.Units);
                break;
            case SourceType.Property:
                ProcessSource(unitListProvider.GetComponent<IUnitListProvider<Unit>>().Units);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        UpdateFromSource();
    }

    private void UpdateFromSource()
    {
        foreach (var contentButton in _contentButtonControllers)
        {
            contentButton.ButtonClicked -= OnUnitButtonClickedInternal;
            Destroy(contentButton.gameObject);
        }

        _contentButtonControllers.Clear();

        foreach (var unit in GetUnitsFromSource())
        {
            var unitButton = Instantiate(unitButtonPrefab, content.transform);
            var buttonController = unitButton.GetComponent<HorizontalUnitListButtonController>();
            buttonController.SetModel(unit);
            buttonController.ButtonClicked += OnUnitButtonClickedInternal;
            _contentButtonControllers.Add(buttonController);
        }
    }

    private IEnumerable<Unit> GetUnitsFromSource()
    {
        return source switch
        {
            SourceType.Tavern => FindObjectOfType<GameService>().TavernService.AvailableUnits,
            SourceType.Barrack => FindObjectOfType<GameService>().BarrackService.AvailableUnits,
            SourceType.Property => unitListProvider.GetComponent<IUnitListProvider<Unit>>().Units,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void ProcessSource<T>(ObservableCollection<T> sourceCollection) where T : Unit
    {
        sourceCollection.CollectionChanged += OnSourceCollectionChanged;
    }

    private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateFromSource();
    }

    private void OnUnitButtonClickedInternal(object sender, Unit unit)
    {
        unitButtonClicked.Invoke(unit);
    }

    public enum SourceType
    {
        Tavern,
        Barrack,
        Property
    }
}