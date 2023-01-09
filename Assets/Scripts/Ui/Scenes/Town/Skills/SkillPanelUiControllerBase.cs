using Model.Units;
using Services;
using Services.Common.Dto;
using UnityEngine;

public abstract class SkillPanelUiControllerBase : MonoBehaviour
{
    private Skills _skills;
    private IBarrackService _barrackService;

    private void Start()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
        SetupButtons();
    }

    public virtual Skills Skills
    {
        set => SetSkills(value);
    }

    protected void SetSkills(Skills value)
    {
        _skills = value;
        ProcessButtons();
    }

    protected TSkills GetSkills<TSkills>() where TSkills : Skills
    {
        return (TSkills)_skills;
    }

    protected abstract void SetupButtons();
    protected abstract void ProcessButtons();

    protected void TrainParamRequest<TSkill, TSkillDto>(
        UnitType type,
        string paramName)
        where TSkill : Skills
        where TSkillDto : SkillsDto
    {
        _barrackService.UpgradeUnitSkill<TSkill, TSkillDto>(
            GetSkills<TSkill>().id,
            type,
            paramName,
            (_, domain) => SetSkills(domain),
            (dto) => (TSkill)dto.ToDomain()
        );
    }
}