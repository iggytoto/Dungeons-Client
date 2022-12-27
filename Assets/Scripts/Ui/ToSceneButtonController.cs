using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSceneButtonController : MonoBehaviour
{

    public string sceneName;
    public string buttonText;
    public TMP_Text buttonTextField;

    private void Start()
    {
        buttonTextField.text = buttonText;
    }

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}