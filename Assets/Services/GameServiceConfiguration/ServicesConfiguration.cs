using System;
using Services;
using Services.Login;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Services Configuration",
    menuName = "Scriptables/Configuration/Services", order = 1)]
public class ServicesConfiguration : ScriptableObject
{
    [SerializeField] private LoginServiceType loginServiceType;
    [SerializeField] private BarrackServiceType barrackServiceType;
    [SerializeField] private MatchMakingServiceType matchMakingServiceType;
    [SerializeField] private PlayerAccountServiceType playerAccountServiceType;
    [SerializeField] private TavernServiceType tavernServiceType;

    public void SetServices(GameService gs)
    {
        SetLoginService(gs);
        SetBarrackService(gs);
        SetMatchMakingService(gs);
        SetPlayerAccountsService(gs);
        SetTavernService(gs);
    }

    private void SetTavernService(GameService gs)
    {
        gs.TavernService = tavernServiceType switch
        {
            TavernServiceType.None => null,
            TavernServiceType.Live => gs.gameObject.AddComponent<UnitShopService>(),
            TavernServiceType.Mocked => new MockUnitShopService(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetPlayerAccountsService(GameService gs)
    {
        gs.PlayerAccountService = playerAccountServiceType switch
        {
            PlayerAccountServiceType.None => null,
            PlayerAccountServiceType.Mocked => new MockPlayerAccountService(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetMatchMakingService(GameService gs)
    {
        gs.MatchMakingService = matchMakingServiceType switch
        {
            MatchMakingServiceType.None => null,
            MatchMakingServiceType.Live => gs.gameObject.AddComponent<MatchMakingService>(),
            MatchMakingServiceType.Mocked => new MockMatchMakingService(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetBarrackService(GameService gs)
    {
        gs.BarrackService = barrackServiceType switch
        {
            BarrackServiceType.None => null,
            BarrackServiceType.Mocked => new MockBarracksService(),
            BarrackServiceType.Live => gs.gameObject.AddComponent<BarracksService>(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetLoginService(GameService gs)
    {
        gs.LoginService = loginServiceType switch
        {
            LoginServiceType.None => null,
            LoginServiceType.Mocked => new MockLoginService(),
            LoginServiceType.AutoReconnect => gs.gameObject.AddComponent<AutoReconnectLoginService>(),
            LoginServiceType.PermanentLogin => gs.gameObject.AddComponent<PermanentLoginService>(),
            LoginServiceType.Live => gs.gameObject.AddComponent<LoginService>(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public enum LoginServiceType
    {
        None,
        Mocked,
        AutoReconnect,
        PermanentLogin,
        Live
    }

    public enum BarrackServiceType
    {
        None,
        Mocked,
        Live
    }

    public enum MatchMakingServiceType
    {
        None,
        Mocked,
        Live
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
        Live
    }
}