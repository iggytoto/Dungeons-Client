using System;
using Services;
using Services.TrainingYard;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Services Configuration",
    menuName = "Scriptables/Configuration/Services", order = 1)]
public class ServicesConfiguration : ScriptableObject
{
    [SerializeField] private LoginServiceType loginServiceType = LoginServiceType.None;
    [SerializeField] private BarrackServiceType barrackServiceType = BarrackServiceType.None;
    [SerializeField] private MatchMakingServiceType matchMakingServiceType = MatchMakingServiceType.None;
    [SerializeField] private PlayerAccountServiceType playerAccountServiceType = PlayerAccountServiceType.None;
    [SerializeField] private TavernServiceType tavernServiceType = TavernServiceType.None;
    [SerializeField] private TrainingYardServiceType trainingYardServiceType = TrainingYardServiceType.None;
    [SerializeField] private string apiHostHttp = "http";
    [SerializeField] private string apiHostAddress = "localhost";
    [SerializeField] private ushort apiHostPort = 8080;

    public void SetServices(GameService gs)
    {
        SetLoginService(gs);
        SetBarrackService(gs);
        SetMatchMakingService(gs);
        SetPlayerAccountsService(gs);
        SetTavernService(gs);
        SetTrainingYardService(gs);
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
                service = new MockUnitShopService();
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
                service = new MockBarracksService();
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
            case LoginServiceType.PermanentLogin:
            case LoginServiceType.Api:
                var s = gs.gameObject.AddComponent<LoginService>();
                s.EndpointAddress = apiHostAddress;
                s.EndpointPort = apiHostPort;
                s.EndpointHttpType = apiHostHttp;
                s.InitService();
                service = s;
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
}