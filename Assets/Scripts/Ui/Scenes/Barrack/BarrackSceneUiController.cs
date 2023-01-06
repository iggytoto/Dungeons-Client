using Model.Units;
using Services;
using UnityEngine;
using UnityEngine.UI;

public class BarrackSceneUiController : MonoBehaviour
{
    [SerializeField] public Button renameUnitButton;
    [SerializeField] public Button changeBattleBehaviorUnitButton;
    [SerializeField] public Button unitEquipmentButton;
    [SerializeField] public UnitEquipmentTableModalController unitEquipmentTableModalController;
    private IBarrackService _barracksService;
    private Unit _selectedUnit;

    private void Start()
    {
        _barracksService = FindObjectOfType<GameService>().BarrackService;
        SetupButtons();
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