using Services;
using UnityEngine;
using UnityEngine.UI;

public class BarrackSceneUiController : MonoBehaviour
{
    [SerializeField] public Unit selectedUnit;
    [SerializeField] public Button trainUnitButton;
    private IBarrackService _barracksService;

    private void Start()
    {
        trainUnitButton.enabled = false;
        trainUnitButton.onClick.AddListener(OnTrainButtonClicked);
        _barracksService = FindObjectOfType<GameService>().BarrackService;
    }

    private void OnTrainButtonClicked()
    {
        if (selectedUnit != null)
        {
            _barracksService.TrainUnit(selectedUnit.Id);
        }
    }

    public void SetSelectedUnit(Unit u)
    {
        selectedUnit = u;
        trainUnitButton.enabled = u is { activity: Unit.UnitActivity.Idle };
    }

    public void ChangeNameUnitName(string newName)
    {
        _barracksService.ChangeUnitName(selectedUnit.Id, newName);
    }
}