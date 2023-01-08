using Model.Units;
using UnityEngine;

public class SelectedUnitSkillsPanelUiController : MonoBehaviour
{
    [SerializeField] private SkillsPanelUiController humanWarriorPanelUiController;

    public Skills Skills
    {
        get => _skills;
        set => SetSkill(value);
    }

    private Skills _skills;

    private void SetSkill(Skills value)
    {
        gameObject.SetActive(value != null);
        _skills = value;
        var controller = SwitchOnSkillsController(value);
        if (controller == null) return;
        controller.Skills = _skills;
        DeactivateControllers();
        controller.gameObject.SetActive(true);
    }

    private void DeactivateControllers()
    {
        humanWarriorPanelUiController.gameObject.SetActive(false);
    }

    private SkillsPanelUiController SwitchOnSkillsController(Skills value)
    {
        if (value is HumanWarriorSkills)
        {
            return humanWarriorPanelUiController;
        }

        return null;
    }
}