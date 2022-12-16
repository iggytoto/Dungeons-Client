using System;
using System.Collections;
using Services;
using Unity.Netcode;
using UnityEngine;

public sealed class TrainingBattleFlowController : NetworkBehaviour
{
    public event Action OnBattleFinished;
    private ITrainingYardService _trainingYardService;

    private void Start()
    {
        _trainingYardService = FindObjectOfType<GameService>().TrainingYardService;
    }

    public async void StartBattle(long userOneId, long userTwoId)
    {
        var rosterOne = await _trainingYardService.GetRosterForUserAsync(userOneId);
        var rosterTwo = await _trainingYardService.GetRosterForUserAsync(userTwoId);
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

    private void SpawnUnits(IEnumerable rosterOne)
    {
        throw new NotImplementedException();
    }
}