using System;
using Services;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class TrainingYardClientFlowController : NetworkBehaviour
{
    private IMatchMakingService _matchMakingService;
    private ILoginService _loginService;

// #if !DEDICATED
    private void Start()
    {
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        _loginService = FindObjectOfType<GameService>().LoginService;

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
    }

    public override void OnNetworkSpawn()
    {
    }
// #endif
}