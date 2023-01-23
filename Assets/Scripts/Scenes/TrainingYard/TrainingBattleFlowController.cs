using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Model.Events;
using Services;
using Unity.Netcode;
using UnityEngine;
using EventType = Model.Events.EventType;
using Random = System.Random;

public sealed class TrainingBattleFlowController : NetworkBehaviour
{
    [SerializeField] private List<GameObject> teamOneSpawnPositions = new();
    [SerializeField] private List<GameObject> teamTwoSpawnPositions = new();

    public event Action OnBattleFinished;

    private readonly List<UnitStateController> _rosterOne = new();
    private readonly List<Unit> _unitsRosterOne = new();
    private readonly List<UnitStateController> _rosterTwo = new();
    private readonly List<Unit> _unitsRosterTwo = new();
    private long _userOneId;
    private long _userTwoId;
    private long _winnerUserId;
    private bool _isBattleInProgress;
    private ResourcesManager _resourcesManager;
    private IEventsService _eventsService;
    private long _eventInstanceId;


    private void Start()
    {
        _resourcesManager = ResourcesManager.GetInstance();
        _eventsService = FindObjectOfType<GameService>().EventsService;
    }

    public void StartBattle(long userOneId, long userTwoId, List<Unit> rosters, long eventInstanceId)
    {
        if (_isBattleInProgress) return;
        _isBattleInProgress = true;
        _userOneId = userOneId;
        _userTwoId = userTwoId;
        _rosterOne.Clear();
        _rosterTwo.Clear();
        _eventInstanceId = eventInstanceId;
        Debug.Log($"Requesting roster for user with id:{userOneId}");
        var rosterOne = rosters.Where(u => u.ownerId == _userOneId).ToList();
        Debug.Log($"Requesting roster for user with id:{userTwoId}");
        var rosterTwo = rosters.Where(u => u.ownerId == _userOneId).ToList();
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
        while (secondsWaited <= 300)
        {
            secondsWaited++;
            if (IsBattleEnded())
            {
                StartCoroutine(EndBattle());
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }

        IsBattleEnded();
        StartCoroutine(EndBattle());
        yield return null;
    }

    private IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(30);
        SaveBattleResult();
        CleanUpObjects();
        OnBattleFinished?.Invoke();
        StopAllCoroutines();
        _isBattleInProgress = false;
        yield return null;
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

        _rosterOne.Clear();
        _rosterTwo.Clear();
    }

    private void SaveBattleResult()
    {
        Debug.Log("Saving results after battle");
        var allUnits = new List<Unit>();
        allUnits.AddRange(_unitsRosterOne);
        allUnits.AddRange(_unitsRosterTwo);
        allUnits = ProcessBattleResultsForUnits(allUnits);
        _eventsService.SaveResult(
            new TrainingMatch3X3EventInstanceResult
            {
                Date = DateTime.Now,
                WinnerId = _winnerUserId,
                UserOneId = _userOneId,
                UserTwoId = _userTwoId,
                EventType = EventType.TrainingMatch3x3,
                UnitsHitPoints = allUnits.ToDictionary(key => key.Id, value => value.hitPoints),
                EventInstanceId = _eventInstanceId
            },
            OnError
        );
    }

    private void OnError(string obj)
    {
        Debug.LogError(obj);
    }

    private List<Unit> ProcessBattleResultsForUnits(List<Unit> allUnits)
    {
        foreach (var unit in allUnits)
        {
            if (unit.hitPoints <= 0)
            {
                unit.hitPoints = 1;
            }
        }

        return allUnits;
    }

    private bool IsBattleEnded()
    {
        var rosterOneDead = _rosterOne.All(x => x.IsDead());
        var rosterTwoDead = _rosterTwo.All(x => x.IsDead());

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
            x => _resourcesManager.LoadUnitPrefabForUnitType(x.type));
        var spawnPositions = new List<GameObject>(playerOne ? teamOneSpawnPositions : teamTwoSpawnPositions);
        foreach (var (unit, prefab) in unitToPrefabMap)
        {
            var rng = new Random();
            var positionIndex = rng.Next(spawnPositions.Count);
            var position = spawnPositions[positionIndex];
            spawnPositions.RemoveAt(positionIndex);
            var go = Instantiate(prefab, position.transform.position, Quaternion.identity);
            var no = go.GetComponent<NetworkObject>();
            var uc = go.GetComponent<UnitStateController>();
            no.Spawn();
            uc.Init(unit);
            if (playerOne)
            {
                _rosterOne.Add(uc);
                _unitsRosterOne.Add(unit);
            }
            else
            {
                _rosterTwo.Add(uc);
                _unitsRosterTwo.Add(unit);
            }
        }
    }
}