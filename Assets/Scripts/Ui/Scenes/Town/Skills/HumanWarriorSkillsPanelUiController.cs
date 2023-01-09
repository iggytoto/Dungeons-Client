using Model.Units;
using Services;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui.Scenes.Town.Skills
{
    public class HumanWarriorSkillsPanelUiController : SkillsPanelUiController
    {
        [SerializeField] private Button trainDefenceButton;
        [SerializeField] private Button trainOffenceButton;

        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite notActiveSprite;

        [SerializeField] private Image defenceL1;
        [SerializeField] private Image defenceL2;
        [SerializeField] private Image defenceL3;
        [SerializeField] private Image defenceL4;
        [SerializeField] private Image defenceL5;
        [SerializeField] private Image offenceL1;
        [SerializeField] private Image offenceL2;
        [SerializeField] private Image offenceL3;
        [SerializeField] private Image offenceL4;
        [SerializeField] private Image offenceL5;

        private HumanWarriorSkills _skills;
        private IBarrackService _barrackService;

        public override Model.Units.Skills Skills
        {
            get => _skills;
            set => SetSkills(value);
        }

        private void Start()
        {
            trainDefenceButton.onClick.AddListener(OnTrainDefenceClicked);
            trainOffenceButton.onClick.AddListener(OnTrainOffenceClicked);
            _barrackService = FindObjectOfType<GameService>().BarrackService;
        }

        private void OnTrainOffenceClicked()
        {
            _barrackService.UpgradeUnitSkill<HumanWarriorSkills, HumanWarriorSkillsDto>(
                _skills.id,
                UnitType.HumanWarrior,
                HumanWarriorSkills.OffenceParamName,
                (_, domain) => SetSkills(domain),
                (dto) => dto.ToDomainTyped()
            );
        }

        private void OnTrainDefenceClicked()
        {
            _barrackService.UpgradeUnitSkill<HumanWarriorSkills, HumanWarriorSkillsDto>(
                _skills.id,
                UnitType.HumanWarrior,
                HumanWarriorSkills.DefenceParamName,
                (_, domain) => SetSkills(domain),
                (dto) => dto.ToDomainTyped()
            );
        }

        private void SetSkills(Model.Units.Skills value)
        {
            _skills = (HumanWarriorSkills)value;
            defenceL1.sprite = _skills.defencePoints == 1 ? activeSprite : notActiveSprite;
            defenceL2.sprite = _skills.defencePoints == 2 ? activeSprite : notActiveSprite;
            defenceL3.sprite = _skills.defencePoints == 3 ? activeSprite : notActiveSprite;
            defenceL4.sprite = _skills.defencePoints == 4 ? activeSprite : notActiveSprite;
            defenceL5.sprite = _skills.defencePoints == 5 ? activeSprite : notActiveSprite;
            offenceL1.sprite = _skills.offencePoints == 1 ? activeSprite : notActiveSprite;
            offenceL2.sprite = _skills.offencePoints == 2 ? activeSprite : notActiveSprite;
            offenceL3.sprite = _skills.offencePoints == 3 ? activeSprite : notActiveSprite;
            offenceL4.sprite = _skills.offencePoints == 4 ? activeSprite : notActiveSprite;
            offenceL5.sprite = _skills.offencePoints == 5 ? activeSprite : notActiveSprite;
            trainDefenceButton.enabled = _skills.defencePoints < 5;
            trainOffenceButton.enabled = _skills.offencePoints < 5;
        }
    }
}