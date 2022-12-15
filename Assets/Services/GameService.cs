using UnityEngine;

namespace Services
{
    public class GameService : MonoBehaviour
    {
        [SerializeField] public string endpointAddress = "http://localhost";
        [SerializeField] public ServicesConfiguration servicesConfiguration;
        public ILoginService LoginService { get; set; }
        public IBarrackService BarrackService { get; set; }
        public IMatchMakingService MatchMakingService { get; set; }
        public IPlayerAccountService PlayerAccountService { get; set; }
        public ITavernService TavernService { get; set; }

        private void Awake()
        {
            servicesConfiguration.SetServices(this);
        }
    }
}