using UnityEngine;

namespace Services
{
    /**
     * Game service is a essential game object for the scene. It holds all the services to interact with the backend.
     * Its recommended to have only one GameService at scene.
     * Basically this is service provider pattern but provides only what should be used by game client from the
     * UI perspective.
     */
    public class GameService : MonoBehaviour
    {
        /**
         * Game service configuration. Gets the configuration and initializes self
         * based on provided config on Awake call;
         */
        [SerializeField] public ServicesConfiguration servicesConfiguration;

        public ILoginService LoginService { get; set; }
        public IBarrackService BarrackService { get; set; }
        public IMatchMakingService MatchMakingService { get; set; }
        public IPlayerAccountService PlayerAccountService { get; set; }
        public ITavernService TavernService { get; set; }
        public ITrainingYardService TrainingYardService { get; set; }

        private void Awake()
        {
            Debug.Log("Configuration start");
            servicesConfiguration.SetServices(this);
            Debug.Log("Configuration finished");
        }
    }
}