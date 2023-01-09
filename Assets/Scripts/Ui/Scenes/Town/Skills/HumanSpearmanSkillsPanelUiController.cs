using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanSpearmanSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button doubleEdgeL1;
        [SerializeField] private Button doubleEdgeL2;
        [SerializeField] private Button doubleEdgeL3;
        [SerializeField] private Button doubleEdgeL4;
        [SerializeField] private Button doubleEdgeL5;
        [SerializeField] private Button midRangeL1;
        [SerializeField] private Button midRangeL2;
        [SerializeField] private Button midRangeL3;
        [SerializeField] private Button midRangeL4;
        [SerializeField] private Button midRangeL5;

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
            doubleEdgeL1.onClick.AddListener(OnTrainDoubleEdgeClicked);
            doubleEdgeL2.onClick.AddListener(OnTrainDoubleEdgeClicked);
            doubleEdgeL3.onClick.AddListener(OnTrainDoubleEdgeClicked);
            doubleEdgeL4.onClick.AddListener(OnTrainDoubleEdgeClicked);
            doubleEdgeL5.onClick.AddListener(OnTrainDoubleEdgeClicked);
            midRangeL1.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeL2.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeL3.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeL4.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeL5.onClick.AddListener(OnTrainMidRangeClicked);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanSpearmanSkills>();
            doubleEdgeL1.enabled = skills.doubleEdgePoints == 0;
            doubleEdgeL2.enabled = skills.doubleEdgePoints == 1;
            doubleEdgeL3.enabled = skills.doubleEdgePoints == 2;
            doubleEdgeL4.enabled = skills.doubleEdgePoints == 3;
            doubleEdgeL5.enabled = skills.doubleEdgePoints == 4;
            midRangeL1.enabled = skills.midRangePoints == 0;
            midRangeL2.enabled = skills.midRangePoints == 1;
            midRangeL3.enabled = skills.midRangePoints == 2;
            midRangeL4.enabled = skills.midRangePoints == 3;
            midRangeL5.enabled = skills.midRangePoints == 4;
        }
    }
}