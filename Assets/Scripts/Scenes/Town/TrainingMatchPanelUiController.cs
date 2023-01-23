using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Model.Events;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Event = Model.Events.Event;
using EventType = Model.Events.EventType;

public class TrainingMatchPanelUiController : UnitListPanelUiController
{
    [SerializeField] private Button registerMatchMakingButton;
    [SerializeField] private Button cancelMatchMakingButton;
    [SerializeField] private TMP_Text matchMakingStatusText;
    [SerializeField] private Button closePanelButton;
    private long? _mmEventId = null;

    private IEventsService _eventsService;

    private void Start()
    {
        _eventsService = FindObjectOfType<GameService>().EventsService;
        registerMatchMakingButton.onClick.AddListener(OnRegisterMatchMakingClicked);
        cancelMatchMakingButton.onClick.AddListener(OnCancelMatchMakingClicked);
        registerMatchMakingButton.enabled = false;
        cancelMatchMakingButton.enabled = false;
        closePanelButton.onClick.AddListener(OnClosePanelButton);
        OnUnitClicked += OnUnitClickedInternal;
    }

    private void OnClosePanelButton()
    {
        OnCancelMatchMakingClicked();
        gameObject.SetActive(false);
        ClearUnits();
    }

    private void OnCancelMatchMakingClicked()
    {
        if (_mmEventId == null) return;
        _eventsService.Cancel(_mmEventId.Value, OnError);
        cancelMatchMakingButton.enabled = false;
        registerMatchMakingButton.enabled = UnitButtonControllers.Any();
        StopAllCoroutines();
    }

    private void OnRegisterMatchMakingClicked()
    {
        _eventsService.Register(
            UnitButtonControllers.Select(ubc => ubc.Unit.Id).ToList(),
            EventType.TrainingMatch3x3,
            OnRegisterSuccessResponse,
            OnError);
        cancelMatchMakingButton.enabled = true;
        registerMatchMakingButton.enabled = false;
    }

    private void OnRegisterSuccessResponse(Event e)
    {
        _mmEventId = e.Id;
        StartCoroutine(WaitForMatchCoroutine());
    }

    private IEnumerator WaitForMatchCoroutine()
    {
        yield return new WaitForSeconds(1);
        _eventsService.Status(UpdateStatusAndConnectIfServerFound, OnError);
    }

    private void UpdateStatusAndConnectIfServerFound(List<EventInstance> obj)
    {
        var ei = obj.FirstOrDefault(e => e.eventType == EventType.TrainingMatch3x3);
        if (ei == null) return;
        StopAllCoroutines();
        SceneManager.LoadScene(SceneConstants.TrainingYardSceneName);
    }

    private void OnError(string errorMessage)
    {
        cancelMatchMakingButton.enabled = false;
        registerMatchMakingButton.enabled = UnitButtonControllers.Any();
        matchMakingStatusText.text = errorMessage;
    }

    public void AddToRoster(Unit u)
    {
        if (!gameObject.activeSelf || UnitButtonControllers.Count > 3)
        {
            return;
        }

        if (UnitButtonControllers.Count == 3) return;
        AddUnit(u);
        if (UnitButtonControllers.Any())
        {
            registerMatchMakingButton.enabled = true;
        }
    }

    private void OnUnitClickedInternal(Unit unit)
    {
        RemoveUnit(unit);
        if (!UnitButtonControllers.Any())
        {
            registerMatchMakingButton.enabled = false;
        }
    }
}