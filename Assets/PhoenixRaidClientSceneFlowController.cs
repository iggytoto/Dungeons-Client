using System;
using System.Linq;
using Model.Events;
using Services;
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
        var eiInfo = _eventsService.EventInfos.FirstOrDefault(ei => ei.eventType == EventType.PhoenixRaid);
        if (eiInfo == null)
        {
            Debug.LogError("There was no event info for PhoenixRaid returning back to Town");
            SceneManager.LoadScene(SceneConstants.TownSceneName);
            return;
        }

        StartNetCodeClient(eiInfo);
    }

    private void StartNetCodeClient(EventInstance ei)
    {
        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.ConnectionData = new UnityTransport.ConnectionAddressData
        {
            Address = ei.host,
            Port = Convert.ToUInt16(ei.port)
        };
        NetworkManager.Singleton.StartClient();
        NetworkManager.Singleton.OnTransportFailure += () => OnNetworkTransportFail(ei);
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private static void OnNetworkTransportFail(EventInstance eventInfo)
    {
        Debug.Log(
            $"Failed to connect to event instance:{eventInfo.id} on server:{eventInfo.host}:{eventInfo.port} with type:{eventInfo.eventType} returning to Town");
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