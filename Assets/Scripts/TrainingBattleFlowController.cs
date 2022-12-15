using System;
using System.Collections;
using System.Collections.ObjectModel;
using Unity.Netcode;
using UnityEngine;

public sealed class TrainingBattleFlowController : NetworkBehaviour
{
    public event Action OnBattleFinished;

    public void StartBattle(long userOneId, long userTwoId)
    {
        var rosterOne = GetRosterForUser(userOneId);
        var rosterTwo = GetRosterForUser(userTwoId);
        SpawnUnits(rosterOne);
        SpawnUnits(rosterTwo);
        StartCoroutine(WaitForBattleEnd());
    }

    private IEnumerator WaitForBattleEnd()
    {
        var secondsWaited = 0;
        while (secondsWaited <= 240)
        {
            secondsWaited++;
            if (IsBattleEnded())
            {
                SaveRosters();
                SaveBattleResult();
                OnBattleFinished?.Invoke();
                StopAllCoroutines();
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void SaveBattleResult()
    {
        throw new NotImplementedException();
    }

    private void SaveRosters()
    {
        throw new NotImplementedException();
    }

    private bool IsBattleEnded()
    {
        throw new NotImplementedException();
    }

    private void SpawnUnits(Collection<Unit> rosterOne)
    {
        throw new NotImplementedException();
    }

    private Collection<Unit> GetRosterForUser(long userOneId)
    {
        throw new NotImplementedException();
    }

    private IEnumerator FakeBattle()
    {
        yield return new WaitForSeconds(20);
        StopAllCoroutines();
        OnBattleFinished?.Invoke();
        yield return null;
    }
}