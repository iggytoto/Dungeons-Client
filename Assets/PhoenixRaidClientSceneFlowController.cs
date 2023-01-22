using System;
using System.Linq;
using Services;
using Services.Events;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Model.Events.EventType;

public class PhoenixRaidClientSceneFlowController : MonoBehaviour
{
    #if !DEDICATED
    private IEventsService _eventsService;

    private void Start()
    {
        _eventsService = FindObjectOfType<GameService>().EventsService;
        var eiInfo = _eventsService.EventInfos.FirstOrDefault(ei => ei.EventType == EventType.PhoenixRaid);
        if (eiInfo == null)
        {
            Debug.LogError("There was no event info for PhoenixRaid returning back to Town");
            SceneManager.LoadScene(SceneConstants.TownSceneName);
            return;
        }

        StartNetCodeClient(eiInfo);
    }

    private void StartNetCodeClient(EventInfo ei)
    {
        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.ConnectionData = new UnityTransport.ConnectionAddressData
        {
            Address = ei.Host,
            Port = Convert.ToUInt16(ei.Port)
        };
        NetworkManager.Singleton.StartClient();
        NetworkManager.Singleton.OnTransportFailure += () => OnNetworkTransportFail(ei);
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private static void OnNetworkTransportFail(EventInfo eventInfo)
    {
        Debug.Log(
            $"Failed to connect to event instance:{eventInfo.EventId} on server:{eventInfo.Host}:{eventInfo.Port} with type:{eventInfo.EventType} returning to Town");
        SceneManager.LoadScene(SceneConstants.TownSceneName);
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