using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanClericSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button disciplineL1;
        [SerializeField] private Button disciplineL2;
        [SerializeField] private Button disciplineL3;
        [SerializeField] private Button disciplineL4;
        [SerializeField] private Button disciplineL5;
        [SerializeField] private Button shatter;
        [SerializeField] private Button divine;
        [SerializeField] private Button purge;

        private void OnTrainPurgeClicked()
        {
            TrainParamRequest<HumanClericSkills, HumanClericSkillsDto>(UnitType.HumanCleric,
                HumanClericSkills.Purge);
        }

        private void OnTrainDivineClicked()
        {
            TrainParamRequest<HumanClericSkills, HumanClericSkillsDto>(UnitType.HumanCleric,
                HumanClericSkills.Divine);
        }

        private void OnTrainShatterClicked()
        {
            TrainParamRequest<HumanClericSkills, HumanClericSkillsDto>(UnitType.HumanCleric,
                HumanClericSkills.Shatter);
        }

        private void OnTrainDisciplineEdgeClicked()
        {
            TrainParamRequest<HumanClericSkills, HumanClericSkillsDto>(UnitType.HumanCleric,
                HumanClericSkills.Discipline);
        }

        protected override void SetupButtons()
        {
            disciplineL1.onClick.AddListener(OnTrainDisciplineEdgeClicked);
            disciplineL2.onClick.AddListener(OnTrainDisciplineEdgeClicked);
            disciplineL3.onClick.AddListener(OnTrainDisciplineEdgeClicked);
            disciplineL4.onClick.AddListener(OnTrainDisciplineEdgeClicked);
            disciplineL5.onClick.AddListener(OnTrainDisciplineEdgeClicked);
            shatter.onClick.AddListener(OnTrainShatterClicked);
            divine.onClick.AddListener(OnTrainDivineClicked);
            purge.onClick.AddListener(OnTrainPurgeClicked);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanClericSkills>();
            disciplineL1.enabled = skills.disciplinePoints == 0;
            disciplineL2.enabled = skills.disciplinePoints == 1;
            disciplineL3.enabled = skills.disciplinePoints == 2;
            disciplineL4.enabled = skills.disciplinePoints == 3;
            disciplineL5.enabled = skills.disciplinePoints == 4;
            shatter.enabled = !skills.shatter;
            divine.enabled = !skills.divine;
            purge.enabled = !skills.purge;
        }
    }
}