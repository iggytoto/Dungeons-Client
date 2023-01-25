using Model.Units;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanArcherSkillsPanelUiController : SkillPanelUiControllerBase
    {
        [SerializeField] private Button midRangeButton;
        [SerializeField] private Button longRangeButton;
        [SerializeField] private Button trainFireArrowsButton;
        [SerializeField] private Button trainPoisonArrowsButton;

        protected override void SetupButtons()
        {
            midRangeButton.onClick.AddListener(OnTrainMidRangeClicked);
            longRangeButton.onClick.AddListener(OnTrainLongRangeClicked);
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
            midRangeButton.enabled = skills.midRangePoints + skills.longRangePoints <= 6;
            longRangeButton.enabled = skills.midRangePoints + skills.longRangePoints <= 6;
            trainFireArrowsButton.enabled = !skills.fireArrows;
            trainPoisonArrowsButton.enabled = !skills.poisonArrows;
        }
    }
}