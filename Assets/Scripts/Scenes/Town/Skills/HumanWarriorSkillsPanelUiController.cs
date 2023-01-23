using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanWarriorSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button defenceL1;
        [SerializeField] private Button defenceL2;
        [SerializeField] private Button defenceL3;
        [SerializeField] private Button defenceL4;
        [SerializeField] private Button defenceL5;
        [SerializeField] private Button offenceL1;
        [SerializeField] private Button offenceL2;
        [SerializeField] private Button offenceL3;
        [SerializeField] private Button offenceL4;
        [SerializeField] private Button offenceL5;

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
            defenceL1.onClick.AddListener(OnTrainDefenceClicked);
            defenceL2.onClick.AddListener(OnTrainDefenceClicked);
            defenceL3.onClick.AddListener(OnTrainDefenceClicked);
            defenceL4.onClick.AddListener(OnTrainDefenceClicked);
            defenceL5.onClick.AddListener(OnTrainDefenceClicked);
            offenceL1.onClick.AddListener(OnTrainOffenceClicked);
            offenceL2.onClick.AddListener(OnTrainOffenceClicked);
            offenceL3.onClick.AddListener(OnTrainOffenceClicked);
            offenceL4.onClick.AddListener(OnTrainOffenceClicked);
            offenceL5.onClick.AddListener(OnTrainOffenceClicked);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanWarriorSkills>();
            defenceL1.enabled = skills.defencePoints == 0;
            defenceL2.enabled = skills.defencePoints == 1;
            defenceL3.enabled = skills.defencePoints == 2;
            defenceL4.enabled = skills.defencePoints == 3;
            defenceL5.enabled = skills.defencePoints == 4;
            offenceL1.enabled = skills.defencePoints == 0;
            offenceL2.enabled = skills.defencePoints == 1;
            offenceL3.enabled = skills.defencePoints == 2;
            offenceL4.enabled = skills.defencePoints == 3;
            offenceL5.enabled = skills.defencePoints == 4;
        }
    }
}