using System.Linq;
using Services;
using Services.Common;
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
    [SerializeField] public float updateInterval = 5;
    private float _updateTime;
    private MatchDto _matchStatus;
    private bool _matchInProgress;

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

    private void Update()
    {
        if (!IsServer) return;
        _updateTime -= Time.deltaTime;
        if (!(_updateTime <= 0)) return;
        if (_loginService.UserContext == null || _loginService.ConnectionState == ConnectionState.Disconnected)
        {
            ProcessLogin();
        }
        else if (_loginService.ConnectionState == ConnectionState.Connecting)
        {
        }
        else
        {
            ProcessMatch();
        }

        _updateTime = updateInterval;
    }

    private void ProcessMatch()
    {
        if (_matchStatus == null)
        {
            _matchMakingService.ApplyForServer(
                host,
                port,
                (_, r) => _matchStatus = r,
                (_, e) => Debug.LogError(e.message));
        }
        else
        {
            if (_matchInProgress || _matchStatus.status != "ServerFound") return;
            _matchInProgress = true;
            Debug.Log("Starting the training...");
            _trainingBattleFlowController.OnBattleFinished += OnBattleFinished;
            _trainingBattleFlowController.StartBattle(
                _matchStatus.userOneId,
                _matchStatus.userTwoId);
        }
    }

    private void ProcessLogin()
    {
        Debug.Log($"Trying to login with credentials: {username}:{password}");
        _loginService.TryLogin(username, password, (_, _) => ProcessMatch());
    }

    private void OnBattleFinished()
    {
        Debug.Log("Battle finished");
        foreach (var clientId in NetworkManager.Singleton.ConnectedClients.Select(c => c.Key))
        {
            Debug.Log($"Disconnecting client with id {clientId}");
            NetworkManager.DisconnectClient(clientId);
        }

        _matchInProgress = false;
        _matchStatus = null;
    }
#endif
}