using Model.Units;
using Services;
using UnityEngine;
using UnityEngine.UI;

public class BarrackSceneUiController : MonoBehaviour
{
    [SerializeField] public Button trainUnitButton;
    [SerializeField] public Button renameUnitButton;
    [SerializeField] public Button changeBattleBehaviorUnitButton;
    [SerializeField] public Button unitEquipmentButton;
    [SerializeField] public UnitEquipmentTableModalController unitEquipmentTableModalController;
    private IBarrackService _barracksService;
    private Unit _selectedUnit;

    private void Start()
    {
        trainUnitButton.enabled = false;
        trainUnitButton.onClick.AddListener(OnTrainButtonClicked);
        _barracksService = FindObjectOfType<GameService>().BarrackService;
        SetupButtons();
    }

    private void OnTrainButtonClicked()
    {
        if (_selectedUnit != null)
        {
            _barracksService.TrainUnit(_selectedUnit.Id);
        }
    }

    public void SetSelectedUnit(Unit u)
    {
        _selectedUnit = u;
        unitEquipmentTableModalController.SetEquipment(u?.equip);
        SetupButtons();
    }

    private void SetupButtons()
    {
        var isButtonEnabled = _selectedUnit is { activity: Unit.UnitActivity.Idle };
        trainUnitButton.enabled = isButtonEnabled;
        renameUnitButton.enabled = isButtonEnabled;
        changeBattleBehaviorUnitButton.enabled = isButtonEnabled;
        unitEquipmentButton.enabled = isButtonEnabled;
    }

    public void ChangeNameUnitName(string newName)
    {
        _barracksService.ChangeUnitName(_selectedUnit.Id, newName);
    }

    public void ChangeUnitBattleBehavior(BattleBehavior bb)
    {
        _barracksService.ChangeUnitBattleBehavior(_selectedUnit.Id, bb);
    }
}