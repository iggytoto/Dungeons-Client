using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanArcherSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button midRangeButtonL1;
        [SerializeField] private Button midRangeButtonL2;
        [SerializeField] private Button midRangeButtonL3;
        [SerializeField] private Button midRangeButtonL4;
        [SerializeField] private Button longRangeButtonL1;
        [SerializeField] private Button longRangeButtonL2;
        [SerializeField] private Button longRangeButtonL3;
        [SerializeField] private Button longRangeButtonL4;
        [SerializeField] private Button trainFireArrowsButton;
        [SerializeField] private Button trainPoisonArrowsButton;

        protected override void SetupButtons()
        {
            midRangeButtonL1.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeButtonL2.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeButtonL3.onClick.AddListener(OnTrainMidRangeClicked);
            midRangeButtonL4.onClick.AddListener(OnTrainMidRangeClicked);

            longRangeButtonL1.onClick.AddListener(OnTrainLongRangeClicked);
            longRangeButtonL2.onClick.AddListener(OnTrainLongRangeClicked);
            longRangeButtonL3.onClick.AddListener(OnTrainLongRangeClicked);
            longRangeButtonL4.onClick.AddListener(OnTrainLongRangeClicked);

            trainFireArrowsButton.onClick.AddListener(OnTrainFireArrowsRangeClicked);
            trainPoisonArrowsButton.onClick.AddListener(OnTrainPoisonRangeClicked);
        }

        private void OnTrainMidRangeClicked()
        {
            TrainParamRequest<HumanArcherSkills, HumanArcherSkillsDto>(UnitType.HumanArcher,
                HumanArcherSkills.MidRangeParamName);
        }

        private void OnTrainLongRangeClicked()
        {
            TrainParamRequest<HumanArcherSkills, HumanArcherSkillsDto>(UnitType.HumanArcher,
                HumanArcherSkills.LongRangeParamName);
        }

        private void OnTrainFireArrowsRangeClicked()
        {
            TrainParamRequest<HumanArcherSkills, HumanArcherSkillsDto>(UnitType.HumanArcher,
                HumanArcherSkills.FireArrowsParamName);
        }

        private void OnTrainPoisonRangeClicked()
        {
            TrainParamRequest<HumanArcherSkills, HumanArcherSkillsDto>(UnitType.HumanArcher,
                HumanArcherSkills.PoisonArrowsParamName);
        }

        protected override void ProcessButtons()
        {
            var skills = GetSkills<HumanArcherSkills>();
            midRangeButtonL1.enabled = skills.midRangePoints == 0;
            midRangeButtonL2.enabled = skills.midRangePoints == 1;
            midRangeButtonL3.enabled = skills.midRangePoints == 2;
            midRangeButtonL4.enabled = skills.midRangePoints == 3;

            longRangeButtonL1.enabled = skills.longRangePoints == 0;
            longRangeButtonL2.enabled = skills.longRangePoints == 1;
            longRangeButtonL3.enabled = skills.longRangePoints == 2;
            longRangeButtonL4.enabled = skills.longRangePoints == 3;

            trainFireArrowsButton.enabled = !skills.fireArrows;
            trainPoisonArrowsButton.enabled = !skills.poisonArrows;
        }
    }
}