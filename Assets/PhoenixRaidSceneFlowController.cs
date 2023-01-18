using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Services;
using Services.Dto;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Model.Events.EventType;
using Random = System.Random;

public class PhoenixRaidSceneFlowController : MonoBehaviour
{
#if DEDICATED

    [SerializeField] private UnitStateController bossController;

    private const int BattleTimeoutSeconds = 300;

    private IEventsService _eventsService;
    private readonly List<Unit> _eventParticipants = new();
    private readonly HashSet<GameObject> _spawnPositions = new();
    private ResourcesManager _resourcesManager;


    private void Start()
    {
        _eventsService = FindObjectOfType<GameService>().EventsService;
        _spawnPositions.AddRange(GameObject.FindGameObjectsWithTag("Respawn"));
        _resourcesManager = ResourcesManager.GetInstance();
        if (_eventsService.EventInfo == null)
        {
            throw new InvalidOperationException("Current event info is null, cannot process empty event");
        }

        if (_eventsService.EventInfo.EventType != EventType.PhoenixRaid)
        {
            throw new InvalidOperationException("Wrong event type, scene cannot process this event type");
        }

        LoadEventInstanceData();
    }

    private void LoadEventInstanceData()
    {
        Debug.Log("Loading event instance data...");
        _eventsService.GetEventInstanceRosters(OnLoadEventInstanceDataSuccess, OnError);
    }

    private void OnError(object sender, ErrorResponseDto e)
    {
        Debug.LogError(e.message);
    }

    private void OnLoadEventInstanceDataSuccess(object sender, List<Unit> e)
    {
        Debug.Log("Data loaded successfully");
        _eventParticipants.Clear();
        _eventParticipants.AddRange(e);
        StartCoroutine(WaitingForPlayersPhase());
    }

    private IEnumerator WaitingForPlayersPhase()
    {
        Debug.Log("Waiting for players to connect...");
        yield return new WaitForSeconds(5);
        SpawnRostersAndStartBattle();
    }

    private void SpawnRostersAndStartBattle()
    {
        SpawnRosters();
        StartCoroutine(WaitForBattleToEnd());
    }

    private void SpawnRosters()
    {
        var unitToPrefabMap =
            _eventParticipants.ToDictionary(x => x, x => _resourcesManager.LoadUnitPrefabForUnitType(x.type));
        var spawnPositions = new List<GameObject>(_spawnPositions);
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
            uc.Init(unit, 1);
        }
    }

    private IEnumerator WaitForBattleToEnd()
    {
        Debug.Log("Waiting for battle to end");
        var secondsWaited = 0;
        while (secondsWaited <= BattleTimeoutSeconds)
        {
            yield return new WaitForSeconds(1);
            secondsWaited++;
            if (IsBattleEndedCondition())
            {
                break;
            }
        }

        ProcessEndBattle();
        yield return new WaitForSeconds(5);
        foreach (var client in NetworkManager.Singleton.ConnectedClients.ToList())
        {
            NetworkManager.Singleton.DisconnectClient(client.Key);
        }
    }

    private bool IsBattleEndedCondition()
    {
        return _eventParticipants.All(u => u.hitPoints <= 0) || bossController.IsDead();
    }

    private void ProcessEndBattle()
    {
        Debug.Log("Processing end of battle");
        SceneManager.LoadScene(SceneConstants.WaitingForEventServerScene);
    }
#endif
}