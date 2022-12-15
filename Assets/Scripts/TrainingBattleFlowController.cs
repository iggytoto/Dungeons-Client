using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public sealed class TrainingBattleFlowController : NetworkBehaviour
{
    public event Action OnBattleFinished;

    public void StartBattle(long matchMakingUserOneId, long matchMakingUserTwoId)
    {
        StartCoroutine(FakeBattle());
    }

    private IEnumerator FakeBattle()
    {
        yield return new WaitForSeconds(20);
        StopAllCoroutines();
        OnBattleFinished?.Invoke();
        yield return null;
    }
}