using System;
using System.Text;
using Services.Dto;
using Services.Login;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoginService : MonoBehaviour
{
    private static UserContext _userContext;
    private EventHandler<UserContext> _onLoginSuccessResponseOuter;
    private LoginServiceApiAdapter _apiAdapter;
    private ConnectionState _connectionState = ConnectionState.Disconnected;

    private void Start()
    {
        _apiAdapter = gameObject.AddComponent<LoginServiceApiAdapter>();
        _apiAdapter.endpointAddress = FindObjectOfType<GameService>().endpointAddress;
    }

    /***
     * Current connection state with login service. 
     */
    public virtual ConnectionState ConnectionState => _connectionState;

    /**
     * Current logged in user information
     */
    public virtual UserContext UserContext => _userContext;

    /***
     * Last text of error was provided by api
     */
    public UnityEvent<string> onLoginMessage = new();

    /**
     * Logins with given credentials, if succeeded navigates to the town scene
     * 
     */
    public virtual void TryLogin(string login, string password, EventHandler<UserContext> onSuccess)
    {
        _connectionState = ConnectionState.Connecting;
        _onLoginSuccessResponseOuter = onSuccess;
        _apiAdapter.Login(login, password, OnLoginSuccess, OnError);
    }

    /**
     * Register user with given credentials.
     * <exception cref="InvalidOperationException">In case called not from Login scene</exception>
     */
    public virtual void Register(string login, string password)
    {
        _apiAdapter.Register(login, password, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(object sender, RegisterResponse e)
    {
        var message = $"Successfully registered with userid {e.userId}";
        onLoginMessage.Invoke(message);
        Debug.Log(message);
    }

    private void OnLoginSuccess(object sender, LoginResponse e)
    {
        _connectionState = ConnectionState.Connected;
        var tokenJson = Encoding.UTF8.GetString(Convert.FromBase64String(e.token));
        _userContext = JsonUtility.FromJson<UserContext>(tokenJson);
        _onLoginSuccessResponseOuter?.Invoke(this, _userContext);
    }

    private void OnError(object sender, ErrorResponse e)
    {
        onLoginMessage.Invoke(e.message);
        Debug.LogError(e.message);
    }
}