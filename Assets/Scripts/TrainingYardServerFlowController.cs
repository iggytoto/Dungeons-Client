using System;
using System.Collections;
using System.Linq;
using Services.Common;
using Unity.Netcode;
using UnityEngine;

public class TrainingYardServerFlowController : NetworkBehaviour
{
    private LoginService _loginService;
    private MatchMakingService _matchMakingService;
    private TrainingBattleFlowController _trainingBattleFlowController;
    private const string Username = "server";
    private const string Password = "password";
    private const string Host = "127.0.0.1";
    private const string Port = "7777";

    private void Start()
    {
        _loginService = FindObjectOfType<LoginService>();
        _matchMakingService = FindObjectOfType<MatchMakingService>();
        _matchMakingService.matchStatusReceived.AddListener(OnMatchStatusReceived);
        _trainingBattleFlowController = FindObjectOfType<TrainingBattleFlowController>();
    }

    private void OnMatchStatusReceived(MatchDto match)
    {
        if (!IsServer)
        {
            return;
        }

        StartCoroutine(StartTrainingYardCombat(match));
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
        while (true)
        {
            switch (_loginService.ConnectionState)
            {
                case ConnectionState.Disconnected:
                    Debug.Log($"Trying to login with credentials: {Username}:{Password}");
                    _loginService.TryLogin(Username, Password, null);
                    break;
                case ConnectionState.Connecting:
                    Debug.Log("Connecting...");
                    break;
                case ConnectionState.Connected:
                    StopAllCoroutines();
                    Debug.Log($"Registering dedicated server as {Host}:{Port}");
                    _matchMakingService.ApplyForServer(Host, Port);
                    yield return null;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator StartTrainingYardCombat(MatchDto match)
    {
        Debug.Log("Waiting for players to connect...");
        yield return new WaitForSeconds(5);
        Debug.Log("Starting the training...");
        StopAllCoroutines();
        _trainingBattleFlowController.OnBattleFinished += OnBattleFinished;
        _trainingBattleFlowController.StartBattle(match.userOneId, match.userTwoId);
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
}