using System;
using Services;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class TrainingYardClientFlowController : NetworkBehaviour
{
    private IMatchMakingService _matchMakingService;

#if !DEDICATED
    private void Start()
    {
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        StartNetCodeClient();
    }

    private void StartNetCodeClient()
    {
        var matchData = _matchMakingService.MatchContext;
        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.ConnectionData = new UnityTransport.ConnectionAddressData
        {
            Address = matchData.serverIpAddress,
            Port = Convert.ToUInt16(matchData.serverPort)
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