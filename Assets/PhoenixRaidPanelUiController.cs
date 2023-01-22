using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Services;
using UnityEngine;
using UnityEngine.UI;
using EventType = Model.Events.EventType;

public class PhoenixRaidPanelUiController : UnitListPanelUiController
{
    [SerializeField] private Button registerRosterButton;
    [SerializeField] private Button cancelButton;
    private IEventsService _eventsService;


    private void Start()
    {
        _eventsService = FindObjectOfType<GameService>().EventsService;
        registerRosterButton.onClick.AddListener(OnRegisterClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
        registerRosterButton.enabled = false;
        cancelButton.enabled = true;
        OnUnitClicked += OnUnitClickedInternal;
    }

    private void OnCancelClicked()
    {
        ClearUnits();
        gameObject.SetActive(false);
    }

    private void OnRegisterClicked()
    {
        _eventsService.Register(UnitButtonControllers.Select(ubc => ubc.Unit.Id).ToList(), EventType.PhoenixRaid,
            OnError);
    }

    private void OnError(string message)
    {
        Debug.LogError(message);
    }

    public void AddToRoster(Unit u)
    {
        if (!gameObject.activeSelf || UnitButtonControllers.Count > 6)
        {
            return;
        }

        AddToContentAsButton(u);
        if (UnitButtonControllers.Any())
        {
            registerRosterButton.enabled = true;
        }
    }

    private void AddToContentAsButton(Unit u)
    {
        if (UnitButtonControllers.Count == 6) return;
        AddUnit(u);
    }

    private void OnUnitClickedInternal(Unit unit)
    {
        RemoveUnit(unit);
        if (!UnitButtonControllers.Any())
        {
            registerRosterButton.enabled = false;
        }
    }
}