using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanWarriorSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button defence;
        [SerializeField] private Button offence;

        private void OnTrainOffenceClicked()
        {
            TrainParamRequest<HumanWarriorSkills, HumanWarriorSkillsDto>(UnitType.HumanWarrior,
                HumanWarriorSkills.OffenceParamName);
        }

        private void OnTrainDefenceClicked()
        {
            TrainParamRequest<HumanWarriorSkills, HumanWarriorSkillsDto>(UnitType.HumanWarrior,
                HumanWarriorSkills.DefenceParamName);
        }

        protected override void SetupButtons()
        {
            defence.onClick.AddListener(OnTrainDefenceClicked);
            offence.onClick.AddListener(OnTrainOffenceClicked);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanWarriorSkills>();
            defence.enabled = skills.defencePoints + skills.offencePoints < 6;
            offence.enabled = skills.defencePoints + skills.offencePoints < 6;
        }
    }
}