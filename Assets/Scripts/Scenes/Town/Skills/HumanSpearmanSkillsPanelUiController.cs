using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanSpearmanSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button doubleEdge;
        [SerializeField] private Button midRange;

        private void OnTrainMidRangeClicked()
        {
            TrainParamRequest<HumanSpearmanSkills, HumanSpearmanSkillsDto>(UnitType.HumanSpearman,
                HumanSpearmanSkills.MidRangeParamName);
        }

        private void OnTrainDoubleEdgeClicked()
        {
            TrainParamRequest<HumanSpearmanSkills, HumanSpearmanSkillsDto>(UnitType.HumanSpearman,
                HumanSpearmanSkills.DoubleEdgeParamName);
        }

        protected override void SetupButtons()
        {
            doubleEdge.onClick.AddListener(OnTrainDoubleEdgeClicked);
            midRange.onClick.AddListener(OnTrainMidRangeClicked);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanSpearmanSkills>();
            doubleEdge.enabled = skills.doubleEdgePoints + skills.midRangePoints < 6;
            midRange.enabled = skills.doubleEdgePoints + skills.midRangePoints < 6;
        }
    }
}