using System;
using System.Text;
using Services.Dto;
using Services.Login;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameService))]
public class LoginService : MonoBehaviour
{
    private static UserContext _userContext;
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
     * <exception cref="InvalidOperationException">In case called not from Login scene</exception>
     */
    public virtual void TryLogin(string login, string password)
    {
        CheckIfLoginScene();

        _connectionState = ConnectionState.Connecting;
        _apiAdapter.Login(login, password, OnLoginSuccess, OnError);
    }

    /**
     * Register user with given credentials.
     * <exception cref="InvalidOperationException">In case called not from Login scene</exception>
     */
    public virtual void Register(string login, string password)
    {
        CheckIfLoginScene();
        _apiAdapter.Register(login, password, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(object sender, RegisterResponse e)
    {
        var message = $"Successfully registered with userid {e.userId}";
        onLoginMessage.Invoke(message);
        Debug.Log(message);
    }

    private static void CheckIfLoginScene()
    {
        if (!SceneManager.GetActiveScene().name.Equals(SceneConstants.LoginSceneName))
        {
            throw new InvalidOperationException("This method should be called only from login scene.");
        }
    }

    private void OnLoginSuccess(object sender, LoginResponse e)
    {
        _connectionState = ConnectionState.Connected;
        var tokenJson = Encoding.UTF8.GetString(Convert.FromBase64String(e.token));
        _userContext = JsonUtility.FromJson<UserContext>(tokenJson);
        ToTownScene();
    }

    private void OnError(object sender, ErrorResponse e)
    {
        onLoginMessage.Invoke(e.message);
        Debug.LogError(e.message);
    }


    protected  void ToTownScene()
    {
        onLoginMessage.Invoke("Login success...");
        SceneManager.LoadScene(SceneConstants.TownSceneName);
    }
}