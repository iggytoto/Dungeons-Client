using UnityEngine;

namespace Services.Login
{
    public class AutoReconnectLoginService : LoginService
    {
        [SerializeField] private string login;
        [SerializeField] private string password;
        [SerializeField] private float reconnectInterval = 10;
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