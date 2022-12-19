using System;
using System.Collections;
using System.Linq;
using Services;
using Unity.Netcode;
using UnityEngine;

public class TrainingYardServerFlowController : NetworkBehaviour
{
    private ILoginService _loginService;
    private IMatchMakingService _matchMakingService;
    private TrainingBattleFlowController _trainingBattleFlowController;
    [SerializeField] public string username = "server";
    [SerializeField] public string password = "password";
    [SerializeField] public string host = "127.0.0.1";
    [SerializeField] public string port = "7777";

#if DEDICATED
    private void Start()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        _trainingBattleFlowController = FindObjectOfType<TrainingBattleFlowController>();

        StartNetCodeServer();
    }

    private void StartNetCodeServer()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.OnClientConnectedCallback += clientId => Debug.Log($"Client connected with id:{clientId}");
        NetworkManager.OnClientDisconnectCallback += clientId => Debug.Log($"Client disconnected with id:{clientId}");
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer && !IsHost)
        {
            StartCoroutine(LoginAndRegisterAsDedicatedServer());
        }
    }

    private IEnumerator LoginAndRegisterAsDedicatedServer()
    {
        if (_loginService == null || _matchMakingService == null)
        {
            Debug.LogError(
                $"Failed to start registration process. Login service :{_loginService}, MM service {_matchMakingService}");
            StopAllCoroutines();
            yield return null;
        }

        while (true)
        {
            switch (_loginService.ConnectionState)
            {
                case ConnectionState.Disconnected:
                    Debug.Log($"Trying to login with credentials: {username}:{password}");
                    _loginService.TryLogin(username, password, null);
                    break;
                case ConnectionState.Connecting:
                    Debug.Log("Connecting...");
                    break;
                case ConnectionState.Connected:
                    StopAllCoroutines();
                    Debug.Log($"Registering dedicated server as {host}:{port}");
                    _matchMakingService.ApplyForServer(host, port);
                    StartCoroutine(WaitForMatch());
                    yield return null;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator WaitForMatch()
    {
        var waitingSeconds = 0;
        while (waitingSeconds <= 60)
        {
            Debug.Log("WaitingForMatch...");
            if (_matchMakingService.MatchContext is { status: "ServerFound" })
            {
                StopAllCoroutines();
                StartCoroutine(StartTrainingYardCombat());
            }

            waitingSeconds++;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator StartTrainingYardCombat()
    {
        Debug.Log("Waiting for players to connect...");
        yield return new WaitForSeconds(5);
        Debug.Log("Starting the training...");
        StopAllCoroutines();
        _trainingBattleFlowController.OnBattleFinished += OnBattleFinished;
        _trainingBattleFlowController.StartBattle(
            _matchMakingService.MatchContext.userOneId,
            _matchMakingService.MatchContext.userTwoId);
        yield return null;
    }

    private void OnBattleFinished()
    {
        Debug.Log("Battle finished");
        foreach (var clientId in NetworkManager.Singleton.ConnectedClients.Select(c => c.Key))
        {
            Debug.Log($"Disconnecting client with id {clientId}");
            NetworkManager.DisconnectClient(clientId);
        }

        StartCoroutine(LoginAndRegisterAsDedicatedServer());
    }
#endif
}