using Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneUiController : MonoBehaviour
{
    public TMP_InputField loginInput;
    public TMP_InputField passInput;
    public TMP_Text messageText;

    private ILoginService _loginService;

    private void Start()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
        messageText.text = "";
    }

    public void OnLoginClick()
    {
        _loginService.TryLogin(loginInput.text, passInput.text, OnLoginSuccess);
    }

    private static void OnLoginSuccess(UserContext e)
    {
        SceneManager.LoadScene(SceneConstants.TownSceneName);
    }

    public void OnRegisterClick()
    {
        _loginService.Register(loginInput.text, passInput.text);
    }

    public void OnLoginMessage(string message)
    {
        messageText.text = message;
    }
}