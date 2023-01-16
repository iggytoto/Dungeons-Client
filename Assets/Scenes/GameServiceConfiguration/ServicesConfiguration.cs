using System;
using Services;
using Services.Events;
using Services.Login;
using Services.TrainingYard;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Services Configuration",
    menuName = "Scriptables/Configuration/Services", order = 1)]
public class ServicesConfiguration : ScriptableObject
{
    [Header("Common")] [SerializeField] private string apiHostHttp = "http";
    [SerializeField] private string apiHostAddress = "localhost";
    [SerializeField] private ushort apiHostPort = 8080;
    [Header("Login")] [SerializeField] private LoginServiceType loginServiceType = LoginServiceType.None;
    [SerializeField] public string login = "server";
    [SerializeField] public string password = "password";
    [SerializeField] public float reconnectInterval = 5;
    [Header("Barrack")] [SerializeField] private BarrackServiceType barrackServiceType = BarrackServiceType.None;

    [Header("MatchMaking")] [SerializeField] [Obsolete]
    private MatchMakingServiceType matchMakingServiceType = MatchMakingServiceType.None;

    [Header("PlayerAccount")] [SerializeField]
    private PlayerAccountServiceType playerAccountServiceType = PlayerAccountServiceType.None;

    [Header("Tavern")] [SerializeField] private TavernServiceType tavernServiceType = TavernServiceType.None;

    [Header("TrainingYard")] [SerializeField]
    private TrainingYardServiceType trainingYardServiceType = TrainingYardServiceType.None;

    [Header("Events")] [SerializeField] private EventsServiceType eventsServiceType = EventsServiceType.None;


    public void SetServices(GameService gs)
    {
        SetLoginService(gs);
        SetBarrackService(gs);
        SetMatchMakingService(gs);
        SetPlayerAccountsService(gs);
        SetTavernService(gs);
        SetTrainingYardService(gs);
        SetEventsService(gs);
    }

    private void SetEventsService(GameService gs)
    {
        IEventsService service;
        switch (eventsServiceType)
        {
            case EventsServiceType.None:
                service = null;
                break;
            case EventsServiceType.Api:
                var s = gs.AddComponent<EventsService>();
                s.EndpointAddress = apiHostAddress;
                s.EndpointPort = apiHostPort;
                s.EndpointHttpType = apiHostHttp;
                s.InitService();
                service = s;
                break;
            case EventsServiceType.Mocked:
                service = new MockEventsService();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.EventsService = service;
    }

    private void SetTrainingYardService(GameService gs)
    {
        ITrainingYardService service;
        switch (trainingYardServiceType)
        {
            case TrainingYardServiceType.None:
                service = null;
                break;
            case TrainingYardServiceType.Mocked:
                service = new MockTrainingYardService();
                break;
            case TrainingYardServiceType.Api:
                var s = gs.AddComponent<TrainingYardService>();
                s.EndpointAddress = apiHostAddress;
                s.EndpointPort = apiHostPort;
                s.EndpointHttpType = apiHostHttp;
                s.InitService();
                service = s;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.TrainingYardService = service;
    }

    private void SetTavernService(GameService gs)
    {
        ITavernService service;
        switch (tavernServiceType)
        {
            case TavernServiceType.None:
                service = null;
                break;
            case TavernServiceType.Mocked:
                service = gs.AddComponent<MockUnitShopService>();
                break;
            case TavernServiceType.Api:
                var s = gs.AddComponent<UnitShopService>();
                s.EndpointAddress = apiHostAddress;
                s.EndpointPort = apiHostPort;
                s.EndpointHttpType = apiHostHttp;
                s.InitService();
                service = s;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.TavernService = service;
    }

    private void SetPlayerAccountsService(GameService gs)
    {
        IPlayerAccountService service = playerAccountServiceType switch
        {
            PlayerAccountServiceType.None => null,
            PlayerAccountServiceType.Mocked => new MockPlayerAccountService(),
            _ => throw new ArgumentOutOfRangeException()
        };
        gs.PlayerAccountService = service;
    }

    [Obsolete]
    private void SetMatchMakingService(GameService gs)
    {
        IMatchMakingService service;
        switch (matchMakingServiceType)
        {
            case MatchMakingServiceType.None:
                service = null;
                break;
            case MatchMakingServiceType.Mocked:
                service = new MockMatchMakingService();
                break;
            case MatchMakingServiceType.Api:
                var s = gs.gameObject.AddComponent<MatchMakingService>();
                s.EndpointAddress = apiHostAddress;
                s.EndpointPort = apiHostPort;
                s.EndpointHttpType = apiHostHttp;
                s.InitService();
                service = s;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.MatchMakingService = service;
    }

    private void SetBarrackService(GameService gs)
    {
        IBarrackService service;
        switch (barrackServiceType)
        {
            case BarrackServiceType.None:
                service = null;
                break;
            case BarrackServiceType.Mocked:
                service = gs.AddComponent<MockBarracksService>();
                break;
            case BarrackServiceType.Api:
                var s = gs.gameObject.AddComponent<BarracksService>();
                s.EndpointAddress = apiHostAddress;
                s.EndpointPort = apiHostPort;
                s.EndpointHttpType = apiHostHttp;
                s.InitService();
                service = s;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.BarrackService = service;
    }

    private void SetLoginService(GameService gs)
    {
        ILoginService service;
        switch (loginServiceType)
        {
            case LoginServiceType.None:
                service = null;
                break;
            case LoginServiceType.Mocked:
                service = new MockLoginService();
                break;
            case LoginServiceType.AutoReconnect:
                var ars = gs.gameObject.AddComponent<AutoReconnectLoginService>();
                ars.EndpointAddress = apiHostAddress;
                ars.EndpointPort = apiHostPort;
                ars.EndpointHttpType = apiHostHttp;
                ars.InitService();
                ars.login = login;
                ars.password = password;
                ars.reconnectInterval = reconnectInterval;
                service = ars;
                break;
            case LoginServiceType.PermanentLogin:
            case LoginServiceType.Api:
                var ls = gs.gameObject.AddComponent<LoginService>();
                ls.EndpointAddress = apiHostAddress;
                ls.EndpointPort = apiHostPort;
                ls.EndpointHttpType = apiHostHttp;
                ls.InitService();
                service = ls;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.LoginService = service;
    }

    public enum LoginServiceType
    {
        None,
        Mocked,
        AutoReconnect,
        PermanentLogin,
        Api
    }

    public enum BarrackServiceType
    {
        None,
        Mocked,
        Api
    }

    public enum MatchMakingServiceType
    {
        None,
        Mocked,
        Api
    }

    public enum PlayerAccountServiceType
    {
        None,
        Mocked
    }

    public enum TavernServiceType
    {
        None,
        Mocked,
        Api
    }

    public enum TrainingYardServiceType
    {
        None,
        Mocked,
        Api
    }

    public enum EventsServiceType
    {
        None,
        Mocked,
        Api
    }
}