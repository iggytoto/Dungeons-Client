using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingYardSceneUiController : MonoBehaviour
{
    public void OnLeaveClicked()
    {
        SceneManager.LoadScene(SceneConstants.TownSceneName);
    }
}
