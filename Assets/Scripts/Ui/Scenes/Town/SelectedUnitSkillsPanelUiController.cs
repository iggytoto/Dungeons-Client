using Model.Units;
using UnityEngine;

public class SelectedUnitSkillsPanelUiController : MonoBehaviour
{
    [SerializeField] private SkillsPanelUiController humanWarriorPanelUiController;

    public Equipment Equipment
    {
        get => _equipment;
        set => SetEquipment(value);
    }

    private Equipment _equipment;

    private void SetEquipment(Equipment value)
    {
        gameObject.SetActive(value != null);
        _equipment = value;
        var controller = SwitchOnSkillsController(value);
        if (controller == null) return;
        controller.Skills = _equipment;
        DeactivateControllers();
        controller.gameObject.SetActive(true);
    }

    private void DeactivateControllers()
    {
        humanWarriorPanelUiController.gameObject.SetActive(false);
    }

    private SkillsPanelUiController SwitchOnSkillsController(Equipment value)
    {
        if (value is HumanWarriorEquipment)
        {
            return humanWarriorPanelUiController;
        }

        return null;
    }
}