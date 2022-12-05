using TMPro;
using UnityEngine;

public class LoginSceneUiController : MonoBehaviour
{
    public TMP_InputField loginInput;
    public TMP_InputField passInput;


    public void OnLoginClick()
    {
        FindObjectOfType<GameService>().GetComponent<LoginService>().TryLogin(loginInput.text, passInput.text);
    }
}