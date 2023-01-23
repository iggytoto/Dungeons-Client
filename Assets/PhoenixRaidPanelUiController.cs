using System.Collections.Specialized;
using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Model.Events;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EventType = Model.Events.EventType;

public class PhoenixRaidPanelUiController : UnitListPanelUiController
{
    [SerializeField] private Button registerRosterButton;
    [SerializeField] private Button connectToEventButton;
    private IEventsService _eventsService;

    private void Start()
    {
        connectToEventButton.gameObject.SetActive(false);
        _eventsService = FindObjectOfType<GameService>().EventsService;
        registerRosterButton.onClick.AddListener(OnRegisterClicked);
        registerRosterButton.enabled = false;
        OnUnitClicked += OnUnitClickedInternal;
        _eventsService.EventInfos.CollectionChanged += (_, events) => OnEventInfosChanged(events);
        connectToEventButton.onClick.AddListener(OnConnectToEventClicked);
    }

    private void OnConnectToEventClicked()
    {
        SceneManager.LoadScene(SceneConstants.PhoenixRaidSceneName);
    }

    private void OnEventInfosChanged(NotifyCollectionChangedEventArgs events)
    {
        if (events.NewItems != null)
        {
            foreach (EventInstance ei in events.NewItems)
            {
                if (ei.eventType != EventType.PhoenixRaid) continue;
                connectToEventButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnRegisterClicked()
    {
        _eventsService.Register(UnitButtonControllers.Select(ubc => ubc.Unit.Id).ToList(), EventType.PhoenixRaid, null,
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