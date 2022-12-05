using System;
using System.Text;
using Services.Dto;
using Services.Login;
using UnityEngine;
using UnityEngine.SceneManagement;

/***
 * Live implementation of login service.
 * Also, this is an interface-mono behaviour - this means that all virtual methods should be treated as interface methods.
 */
public class LoginService : MonoBehaviour
{
    [SerializeField] public string endpointAddress = "http://localhost";

    private static UserContext _userContext;
    private LoginServiceApiAdapter _apiAdapter;
    private ConnectionState _connectionState = ConnectionState.Disconnected;

    private void Start()
    {
        _apiAdapter = gameObject.AddComponent<LoginServiceApiAdapter>();
        _apiAdapter.endpointAddress = endpointAddress;
    }

    /***
     * Current connection state with login service. 
     */
    public virtual ConnectionState ConnectionState => _connectionState;

    /**
     * Current logged in user information
     */
    public virtual UserContext UserContext => _userContext;

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
        Debug.Log($"Successfully registered with userid {e.userId}");
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
        Debug.LogError(e.message);
    }


    protected static void ToTownScene()
    {
        SceneManager.LoadScene(SceneConstants.TownSceneName);
    }
}