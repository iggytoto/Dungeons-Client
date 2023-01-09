using Model.Units;
using Services;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanArcherSkillsPanelUiController : SkillsPanelUiController
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

        private HumanArcherSkills _skills;
        private IBarrackService _barrackService;

        public override Model.Units.Skills Skills
        {
            get => _skills;
            set => SetSkills(value);
        }

        private void Start()
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
            _barrackService = FindObjectOfType<GameService>().BarrackService;
        }

        private void OnTrainMidRangeClicked()
        {
            TrainParamRequest(HumanArcherSkills.MidRangeParamName);
        }

        private void OnTrainLongRangeClicked()
        {
            TrainParamRequest(HumanArcherSkills.LongRangeParamName);
        }

        private void OnTrainFireArrowsRangeClicked()
        {
            TrainParamRequest(HumanArcherSkills.FireArrowsParamName);
        }

        private void OnTrainPoisonRangeClicked()
        {
            TrainParamRequest(HumanArcherSkills.PoisonArrowsParamName);
        }

        private void TrainParamRequest(string paramName)
        {
            _barrackService.UpgradeUnitSkill<HumanArcherSkills, HumanArcherSkillsDto>(
                _skills.id,
                UnitType.HumanArcher,
                paramName,
                (_, domain) => SetSkills(domain),
                (dto) => dto.ToDomainTyped()
            );
        }

        private void SetSkills(Model.Units.Skills value)
        {
            _skills = (HumanArcherSkills)value;

            midRangeButtonL1.enabled = _skills.midRangePoints == 0;
            midRangeButtonL2.enabled = _skills.midRangePoints == 1;
            midRangeButtonL3.enabled = _skills.midRangePoints == 2;
            midRangeButtonL4.enabled = _skills.midRangePoints == 3;

            longRangeButtonL1.enabled = _skills.longRangePoints == 0;
            longRangeButtonL2.enabled = _skills.longRangePoints == 1;
            longRangeButtonL3.enabled = _skills.longRangePoints == 2;
            longRangeButtonL4.enabled = _skills.longRangePoints == 3;

            trainFireArrowsButton.enabled = !_skills.fireArrows;
            trainPoisonArrowsButton.enabled = !_skills.poisonArrows;
        }
    }
}