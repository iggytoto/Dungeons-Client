using System;
using System.Collections.Generic;
using System.Linq;
using Services;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Model.Events.EventType;

public class TrainingYardServerFlowController : NetworkBehaviour
{
    private IEventsService _eventsService;
    private TrainingBattleFlowController _trainingBattleFlowController;
    private float _updateTime;

#if DEDICATED

    private void Start()
    {
        _eventsService = FindObjectOfType<GameService>().EventsService;
        _trainingBattleFlowController = FindObjectOfType<TrainingBattleFlowController>();
        var eventInstance =
            _eventsService.EventInstances.FirstOrDefault(ei => ei.eventType == EventType.TrainingMatch3x3);
        if (eventInstance == null)
        {
            throw new InvalidOperationException("Current event info is null, cannot process empty event");
        }

        Debug.Log("Loading event instance data...");
        _eventsService.GetEventInstanceRosters(eventInstance.id, OnLoadEventInstanceDataSuccess, OnError);
    }

    private void OnError(string obj)
    {
        Debug.LogError(obj);
    }

    private void OnLoadEventInstanceDataSuccess(List<Unit> eventData)
    {
        var userIds = eventData.Select(unit => unit.ownerId).Distinct().ToList();
        if (userIds.Count != 2)
        {
            Debug.LogError("found more than 2 userids in units");
        }

        _trainingBattleFlowController.StartBattle(userIds[0], userIds[1], eventData,
            _eventsService.EventInstances.First(ei => ei.eventType == EventType.TrainingMatch3x3).id);
        _trainingBattleFlowController.OnBattleFinished += OnBattleFinished;
    }

    private void OnBattleFinished()
    {
        Debug.Log("Battle finished");
        foreach (var clientId in NetworkManager.Singleton.ConnectedClients.Select(c => c.Key))
        {
            Debug.Log($"Disconnecting client with id {clientId}");
            NetworkManager.DisconnectClient(clientId);
        }

        SceneManager.LoadScene(SceneConstants.WaitingForEventServerScene);
    }
#endif
}