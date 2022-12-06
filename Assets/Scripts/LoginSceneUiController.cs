using System;
using TMPro;
using UnityEngine;

public class LoginSceneUiController : MonoBehaviour
{
    public TMP_InputField LoginInput;
    public TMP_InputField PassInput;
    public TMP_Text MessageText;

    private LoginService _loginService;

    private void Start()
    {
        _loginService = FindObjectOfType<LoginService>();
        MessageText.text = "";
    }

    public void OnLoginClick()
    {
        _loginService.TryLogin(LoginInput.text, PassInput.text);
    }

    public void OnRegisterClick()
    {
        _loginService.Register(LoginInput.text, PassInput.text);
    }

    public void OnLoginMessage(string message)
    {
        MessageText.text = message;
    }
}