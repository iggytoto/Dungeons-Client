using System;
using System.Linq;
using Services;
using Services.Common;
using Services.Dto;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Model.Events.EventType;

public class TrainingYardClientFlowController : NetworkBehaviour
{
#if !DEDICATED
    private IEventsService _eventsService;

    private void Start()
    {
        _eventsService = FindObjectOfType<GameService>().EventsService;
        var eventInstance = _eventsService.EventInfos.FirstOrDefault(ei => ei.eventType == EventType.TrainingMatch3x3);
        if (eventInstance == null)
        {
            Debug.LogWarning("there is not event instance for training match 3x3, returning back to town");
            SceneManager.LoadScene(SceneConstants.TownSceneName);
            return;
        }

        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.ConnectionData = new UnityTransport.ConnectionAddressData
        {
            Address = eventInstance.host,
            Port = Convert.ToUInt16(eventInstance.port)
        };
        NetworkManager.Singleton.StartClient();
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            return;
        }

        ShowBattleResultsPanel();
    }

    private void ShowBattleResultsPanel()
    {
    }
#endif
}