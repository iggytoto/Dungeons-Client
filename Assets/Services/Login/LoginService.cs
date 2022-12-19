using System;
using System.Text;
using Services;
using Services.Dto;
using Services.Login;
using UnityEngine;

public class LoginService : ServiceBase, ILoginService
{
    private static UserContext _userContext;
    private EventHandler<UserContext> _onLoginSuccessResponseOuter;
    private LoginServiceApiAdapter _apiAdapter;

    public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

    public UserContext UserContext => _userContext;

    public override void InitService()
    {
        _apiAdapter = gameObject.AddComponent<LoginServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
        Debug.Log(
            $"Login service adapter configured with endpoint:{_apiAdapter.GetConnectionAddress()}");
    }

    public void TryLogin(string login, string password, EventHandler<UserContext> onSuccess)
    {
        if (_apiAdapter == null)
        {
            Debug.LogWarning("TryLogin method called before");
        }

        ConnectionState = ConnectionState.Connecting;
        _onLoginSuccessResponseOuter = onSuccess;
        _apiAdapter.Login(login, password, OnLoginSuccess, OnError);
    }

    public void Register(string login, string password)
    {
        _apiAdapter.Register(login, password, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(object sender, RegisterResponse e)
    {
        var message = $"Successfully registered with userid {e.userId}";
        Debug.Log(message);
    }

    private void OnLoginSuccess(object sender, LoginResponse e)
    {
        ConnectionState = ConnectionState.Connected;
        var tokenJson = Encoding.UTF8.GetString(Convert.FromBase64String(e.token));
        _userContext = JsonUtility.FromJson<UserContext>(tokenJson);
        _onLoginSuccessResponseOuter?.Invoke(this, _userContext);
    }

    private void OnError(object sender, ErrorResponseDto e)
    {
        ConnectionState = ConnectionState.Disconnected;
        Debug.LogError(e.message);
    }
}