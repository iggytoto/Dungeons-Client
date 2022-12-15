using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DefaultNamespace;
using Services;
using Services.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingUnitSelectionModalController : MonoBehaviour, IUnitListProvider<Unit>
{
    public ObservableCollection<Unit> Units { get; } = new();

    private IBarrackService _barrackService;
    private IMatchMakingService _matchMakingService;
    private readonly List<Unit> _selectedUnits = new();

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
        _matchMakingService.Register(_selectedUnits.Select(u => u.Id));
        StartCoroutine(WaitForMatch());
    }

    private IEnumerator WaitForMatch()
    {
        while (true)
        {
            if (_matchMakingService.MatchContext is { status: "ServerFound" })
            {
                StopAllCoroutines();
                SceneManager.LoadScene(SceneConstants.TrainingYardSceneName);
            }

            yield return new WaitForSeconds(1);
        }
    }
}