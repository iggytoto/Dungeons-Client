using System;
using Services;
using Services.Common;
using Services.Dto;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingYardClientFlowController : NetworkBehaviour
{
#if !DEDICATED
    private IMatchMakingService _matchMakingService;
    private void Start()
    {
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        _matchMakingService.Status((_, r) => StartNetCodeClient(r), OnFailedToGetMatchStatus);
    }

    private static void OnFailedToGetMatchStatus(object sender, ErrorResponseDto e)
    {
        Debug.LogError("Failed to get match status, returning to the town scene");
        SceneManager.LoadScene(SceneConstants.TownSceneName);
    }

    private void StartNetCodeClient(MatchDto match)
    {
        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.ConnectionData = new UnityTransport.ConnectionAddressData
        {
            Address = match.serverIpAddress,
            Port = Convert.ToUInt16(match.serverPort)
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