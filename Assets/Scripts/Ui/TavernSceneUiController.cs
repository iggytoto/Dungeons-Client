using Services;
using UnityEngine;

public class TavernSceneUiController : MonoBehaviour
{
    private ITavernService _tavernService;
    [SerializeField] private SelectedUnitPanelController selectedUnitPanelController;
    private Unit _selectedUnit;

    private void Start()
    {
        _tavernService = FindObjectOfType<GameService>().TavernService;
    }


    public void OnTavernUnitClicked(Unit unit)
    {
        _selectedUnit = unit;
        selectedUnitPanelController.SetSelectedUnit(unit);
    }

    public void BuySelectedUnit()
    {
        if (_selectedUnit != null)
        {
            _tavernService.BuyUnit(_selectedUnit);
        }
    }
}