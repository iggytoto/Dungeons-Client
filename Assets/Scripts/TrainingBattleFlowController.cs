using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Services;
using Services.Common;
using Unity.Netcode;
using UnityEngine;
using Random = System.Random;

public sealed class TrainingBattleFlowController : NetworkBehaviour
{
    [SerializeField] private List<GameObject> TeamOneSpawnPositions = new();
    [SerializeField] private List<GameObject> TeamTwoSpawnPositions = new();

    public event Action OnBattleFinished;
    private ITrainingYardService _trainingYardService;
    private readonly List<UnitController> _rosterOne = new();
    private readonly List<UnitController> _rosterTwo = new();
    private long _userOneId;
    private long _userTwoId;
    private long _winnerUserId;
    private bool _isBattleInProgress;


    private void Start()
    {
        _trainingYardService = FindObjectOfType<GameService>().TrainingYardService;
    }

    public async void StartBattle(long userOneId, long userTwoId)
    {
        if (_isBattleInProgress) return;
        _isBattleInProgress = true;
        _userOneId = userOneId;
        _userTwoId = userTwoId;
        _rosterOne.Clear();
        _rosterTwo.Clear();
        Debug.Log($"Requesting roster for user with id:{userOneId}");
        var rosterOne = await _trainingYardService.GetRosterForUserAsync(userOneId);
        Debug.Log($"Requesting roster for user with id:{userTwoId}");
        var rosterTwo = await _trainingYardService.GetRosterForUserAsync(userTwoId);
        Debug.Log($"Spawning roster for user with id:{userOneId}");
        SpawnUnits(rosterOne, true);
        Debug.Log($"Spawning roster for user with id:{userTwoId}");
        SpawnUnits(rosterTwo, false);
        Debug.Log("Waiting for battle to finish...");
        StartCoroutine(WaitForBattleEnd());
    }

    private IEnumerator WaitForBattleEnd()
    {
        var secondsWaited = 0;
        while (secondsWaited <= 20)
        {
            secondsWaited++;
            if (IsBattleEnded())
            {
                SaveRosters();
                SaveBattleResult();
                CleanUpObjects();
                OnBattleFinished?.Invoke();
                StopAllCoroutines();
                _isBattleInProgress = false;
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void CleanUpObjects()
    {
        foreach (var go in _rosterOne.Select(uc => uc.gameObject))
        {
            Destroy(go);
        }

        foreach (var go in _rosterTwo.Select(uc => uc.gameObject))
        {
            Destroy(go);
        }
    }

    private void SaveBattleResult()
    {
        Debug.Log("Saving battle result");
        _trainingYardService.SaveTrainingResult(new MatchResultDto
        {
            date = DateTime.Now,
            matchType = "MatchMaking3x3",
            userOneId = _userOneId,
            userTwoId = _userTwoId,
            winnerUserId = _winnerUserId
        });
    }

    private void SaveRosters()
    {
        Debug.Log("Saving rosters after battle");
        var allUnits = new List<Unit>();
        allUnits.AddRange(_rosterOne.Select(x => x.ToUnit()));
        allUnits.AddRange(_rosterTwo.Select(x => x.ToUnit()));
        _trainingYardService.SaveRosters(ProcessBattleResultsForUnits(allUnits));
    }

    private IEnumerable<Unit> ProcessBattleResultsForUnits(List<Unit> allUnits)
    {
        foreach (var unit in allUnits)
        {
            unit.TrainingExperience += 100;
        }

        return allUnits;
    }

    private bool IsBattleEnded()
    {
        var rosterOneDead = _rosterOne.All(x => x.Unit.IsDead());
        var rosterTwoDead = _rosterTwo.All(x => x.Unit.IsDead());

        switch (rosterOneDead)
        {
            case true when !rosterTwoDead:
                _winnerUserId = _userOneId;
                return true;
            case false when rosterTwoDead:
                _winnerUserId = _userTwoId;
                return true;
            default:
                return false;
        }
    }

    private void SpawnUnits(IEnumerable<Unit> roster, bool playerOne)
    {
        var unitToPrefabMap = roster.ToDictionary(x => x,
            x => ResourcesManager.LoadPrefabForUnitType(x.Type));
        var spawnPositions = new List<GameObject>(playerOne ? TeamOneSpawnPositions : TeamTwoSpawnPositions);
        foreach (var (unit, prefab) in unitToPrefabMap)
        {
            var rng = new Random();
            var positionIndex = rng.Next(spawnPositions.Count);
            var position = spawnPositions[positionIndex];
            spawnPositions.RemoveAt(positionIndex);
            var go = Instantiate(prefab, position.transform.position, Quaternion.identity);
            var no = go.GetComponent<NetworkObject>();
            var uc = go.GetComponent<UnitController>();
            no.Spawn();
            uc.Init(unit);
            if (playerOne)
            {
                _rosterOne.Add(uc);
            }
            else
            {
                _rosterTwo.Add(uc);
            }
        }
    }
}