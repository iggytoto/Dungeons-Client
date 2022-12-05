using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSceneButtonController : MonoBehaviour
{

    public string sceneName;
    public TMP_Text buttonText;

    private void Start()
    {
        buttonText.text = sceneName;
    }

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}