using System;
using Services;
using Services.Events;
using Services.Login;
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

    [Header("PlayerAccount")] [SerializeField]
    private PlayerAccountServiceType playerAccountServiceType = PlayerAccountServiceType.None;

    [Header("Tavern")] [SerializeField] private TavernServiceType tavernServiceType = TavernServiceType.None;

    [Header("Events")] [SerializeField] private EventsServiceType eventsServiceType = EventsServiceType.None;


    public void SetServices(GameService gs)
    {
        SetLoginService(gs);
        SetBarrackService(gs);
        SetPlayerAccountsService(gs);
        SetTavernService(gs);
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
                service = gs.AddComponent<MockEventsService>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gs.EventsService = service;
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

    public enum EventsServiceType
    {
        None,
        Mocked,
        Api
    }
}