using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanClericSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button discipline;
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
            discipline.onClick.AddListener(OnTrainDisciplineEdgeClicked);
            shatter.onClick.AddListener(OnTrainShatterClicked);
            divine.onClick.AddListener(OnTrainDivineClicked);
            purge.onClick.AddListener(OnTrainPurgeClicked);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanClericSkills>();
            discipline.enabled = skills.disciplinePoints < 5 && skills.disciplinePoints + (skills.shatter ? 1 : 0) +
                (skills.divine ? 1 : 0) + (skills.purge ? 1 : 0) < 6;
            shatter.enabled = !skills.shatter;
            divine.enabled = !skills.divine;
            purge.enabled = !skills.purge;
        }
    }
}