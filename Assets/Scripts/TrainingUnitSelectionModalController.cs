using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DefaultNamespace;
using Services;
using Services.Common;
using Services.Dto;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingUnitSelectionModalController : MonoBehaviour, IUnitListProvider<Unit>
{
    public ObservableCollection<Unit> Units { get; } = new();
    [SerializeField] public float refreshInterval = 5;


    private IBarrackService _barrackService;
    private IMatchMakingService _matchMakingService;
    private readonly List<Unit> _selectedUnits = new();
    private bool _isMmSubmitted;
    private float _refreshTimer;

    private void Awake()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
    }

    public void ShowModal()
    {
        gameObject.SetActive(true);
        Units.Clear();
        Units.AddRange(_barrackService.AvailableUnits.Where(u => u.Activity == Unit.UnitActivity.Idle));
    }

    public void HideModal()
    {
        StopAllCoroutines();
        _matchMakingService.Cancel();
        gameObject.SetActive(false);
    }

    public void OnUnitSelected(Unit u)
    {
        if (_selectedUnits.Count > 2)
        {
            return;
        }

        Units.Remove(u);
        _selectedUnits.Add(u);
    }

    public void OnSubmitMatchMaking()
    {
        if (_isMmSubmitted) return;
        _isMmSubmitted = true;
        _matchMakingService.Register(_selectedUnits.Select(u => u.Id), OnMmStatusReceived, OnError);
    }

    private void Update()
    {
        _refreshTimer -= Time.deltaTime;
        if (!(_refreshTimer <= 0) || !_isMmSubmitted) return;
        _matchMakingService.Status(OnMmStatusReceived, OnError);
        _refreshTimer = refreshInterval;
    }

    private void OnMmStatusReceived(object sender, MatchDto e)
    {
        if (e == null) return;
        switch (e.status)
        {
            case "ServerFound":
                SceneManager.LoadScene(SceneConstants.TrainingYardSceneName);
                break;
        }
    }

    private static void OnError(object sender, ErrorResponseDto e)
    {
        Debug.LogError(e.message);
    }
}