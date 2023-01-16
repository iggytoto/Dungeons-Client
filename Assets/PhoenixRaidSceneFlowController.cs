using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoenixRaidSceneFlowController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitingForPlayersPhase());
    }

    private IEnumerator WaitingForPlayersPhase()
    {
        Debug.Log("Waiting for players to connect");
        yield return new WaitForSeconds(5);
        SpawnRostersAndStartBattle();
    }

    private void SpawnRostersAndStartBattle()
    {
        Debug.Log("Waiting for players to connect");
        StartCoroutine(WaitForBattleToEnd());
    }

    private IEnumerator WaitForBattleToEnd()
    {
        Debug.Log("Waiting for battle to end");
        yield return new WaitForSeconds(5);
        ProcessEndBattle();
    }

    private void ProcessEndBattle()
    {
        Debug.Log("Processing end of battle");
        SceneManager.LoadScene(SceneConstants.WaitingForEventServerScene);
    }
}