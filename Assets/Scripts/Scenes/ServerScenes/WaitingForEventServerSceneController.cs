using System;
using Model.Events;
using Services;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Model.Events.EventType;

public class WaitingForEventServerSceneController : NetworkBehaviour
{
    private ILoginService _loginService;
    private IEventsService _eventsService;
    [SerializeField] public string username = "server";
    [SerializeField] public string password = "password";
    [SerializeField] public string host = "127.0.0.1";
    [SerializeField] public string port = "7777";
    [SerializeField] public float updateInterval = 5;
    private float _updateTime;
    private EventInstance _eventInstance;

#if DEDICATED
    private void Start()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
        _eventsService = FindObjectOfType<GameService>().EventsService;

        StartNetCodeServer();
    }

    private void StartNetCodeServer()
    {
        NetworkManager.Singleton.StartServer();
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
            ProcessEvent();
        }

        _updateTime = updateInterval;
    }

    private void ProcessEvent()
    {
        if (_eventInstance == null)
        {
            _eventsService.ApplyAsServer(
                host,
                port,
                (_, r) => _eventInstance = r,
                (_, e) => Debug.LogError(e.message));
        }
        else
        {
            switch (_eventInstance.EventType)
            {
                case EventType.PhoenixRaid:
                    SceneManager.LoadScene(SceneConstants.PhoenixRaidSceneName);
                    break;
                case EventType.MatchMaking3x3:
                    SceneManager.LoadScene(SceneConstants.TrainingYardSceneName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void ProcessLogin()
    {
        Debug.Log($"Trying to login with credentials: {username}:{password}");
        _loginService.TryLogin(username, password, (_, _) => ProcessEvent());
    }
#endif
}