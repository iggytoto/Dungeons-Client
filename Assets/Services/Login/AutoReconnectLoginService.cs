using UnityEngine;

namespace Services.Login
{
    public class AutoReconnectLoginService : LoginService
    {
        [SerializeField] public string login ;
        [SerializeField] public string password ;
        [SerializeField] public float reconnectInterval = 5;
        private float _currentReconnectTimer;

        private void Update()
        {
            _currentReconnectTimer -= Time.deltaTime;
            if (_currentReconnectTimer <= 0 && ConnectionState == ConnectionState.Disconnected)
            {
                TryReconnect();
            }
        }

        private void TryReconnect()
        {
            TryLogin(login, password, null);
            _currentReconnectTimer = reconnectInterval;
        }
    }
}