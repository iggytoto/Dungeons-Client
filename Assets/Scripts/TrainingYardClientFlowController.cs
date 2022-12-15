using Services;
using Unity.Netcode;

public class TrainingYardClientFlowController : NetworkBehaviour
{
    private IMatchMakingService _matchMakingService;
    private ILoginService _loginService;

    private void Start()
    {
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        _loginService = FindObjectOfType<GameService>().LoginService;
#if !DEDICATED
        NetworkManager.Singleton.StartClient();
#endif
    }

    public override void OnNetworkSpawn()
    {
        
    }
}