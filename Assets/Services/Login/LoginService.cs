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
    private ConnectionState _connectionState = ConnectionState.Disconnected;

    private void Start()
    {
        _apiAdapter = gameObject.AddComponent<LoginServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
    }

    public ConnectionState ConnectionState => _connectionState;

    public UserContext UserContext => _userContext;

    public void TryLogin(string login, string password, EventHandler<UserContext> onSuccess)
    {
        _connectionState = ConnectionState.Connecting;
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
        _connectionState = ConnectionState.Connected;
        var tokenJson = Encoding.UTF8.GetString(Convert.FromBase64String(e.token));
        _userContext = JsonUtility.FromJson<UserContext>(tokenJson);
        _onLoginSuccessResponseOuter?.Invoke(this, _userContext);
    }

    private void OnError(object sender, ErrorResponseDto e)
    {
        _connectionState = ConnectionState.Disconnected;
        Debug.LogError(e.message);
    }
}