using System;
using System.Text;
using Services;
using Services.Dto;
using Services.Login;
using UnityEngine;

public class LoginService : ServiceBase, ILoginService
{
    private const string LoginPath = "/auth/login";
    private const string RegisterPath = "/auth/register";

    private static UserContext _userContext;
    private EventHandler<UserContext> _onLoginSuccessResponseOuter;

    public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

    public UserContext UserContext => _userContext;

    public new void InitService()
    {
        Debug.Log(
            $"Login service adapter configured with endpoint:{APIAdapter.GetConnectionAddress()}");
    }

    public void TryLogin(
        string login,
        string password,
        EventHandler<UserContext> onSuccess)
    {
        if (APIAdapter == null)
        {
            Debug.LogWarning("TryLogin method called before");
        }

        ConnectionState = ConnectionState.Connecting;
        _onLoginSuccessResponseOuter = onSuccess;
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<LoginResponseDto>(
                APIAdapter.GetConnectionAddress() + LoginPath,
                ApiAdapter.SerializeDto(new LoginRequestDto { login = login, password = password }),
                ApiAdapter.Post,
                OnLoginSuccess,
                OnError));
    }

    public void Register(string login, string password)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<RegisterResponseDto>(
                APIAdapter.GetConnectionAddress() + RegisterPath,
                ApiAdapter.SerializeDto(new RegisterRequestDto { login = login, password = password }),
                ApiAdapter.Post,
                OnRegisterSuccess,
                OnError));
    }

    private void OnRegisterSuccess(object sender, RegisterResponseDto e)
    {
        var message = $"Successfully registered with userid {e.userId}";
        Debug.Log(message);
    }

    private void OnLoginSuccess(object sender, LoginResponseDto e)
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