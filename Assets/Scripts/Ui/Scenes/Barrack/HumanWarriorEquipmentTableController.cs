using DefaultNamespace;
using Model.Units;
using Services;
using Services.Common.Dto;
using Services.Dto;
using UnityEngine;
using UnityEngine.UI;

public class HumanWarriorEquipmentTableController : MonoBehaviour, IEquipmentTableController
{
    [SerializeField] private Button upgradeOffenceButton;
    [SerializeField] private Button upgradeDefenceButton;

    [SerializeField] private Sprite currentLevelSprite;
    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private Image defenceLevel1;
    [SerializeField] private Image defenceLevel2;
    [SerializeField] private Image defenceLevel3;
    [SerializeField] private Image defenceLevel4;
    [SerializeField] private Image defenceLevel5;
    [SerializeField] private Image offenceLevel1;
    [SerializeField] private Image offenceLevel2;
    [SerializeField] private Image offenceLevel3;
    [SerializeField] private Image offenceLevel4;
    [SerializeField] private Image offenceLevel5;

    private HumanWarriorEquipment _equipment;
    private IBarrackService _barrackService;

    private void Awake()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
    }

    private void ProcessButtons()
    {
        upgradeDefenceButton.enabled = _equipment.defencePoints < 5;
        upgradeOffenceButton.enabled = _equipment.offencePoints < 5;

        offenceLevel1.sprite = _equipment.offencePoints == 1 ? currentLevelSprite : defaultSprite;
        offenceLevel2.sprite = _equipment.offencePoints == 2 ? currentLevelSprite : defaultSprite;
        offenceLevel3.sprite = _equipment.offencePoints == 3 ? currentLevelSprite : defaultSprite;
        offenceLevel4.sprite = _equipment.offencePoints == 4 ? currentLevelSprite : defaultSprite;
        offenceLevel5.sprite = _equipment.offencePoints == 5 ? currentLevelSprite : defaultSprite;
        defenceLevel1.sprite = _equipment.defencePoints == 1 ? currentLevelSprite : defaultSprite;
        defenceLevel2.sprite = _equipment.defencePoints == 2 ? currentLevelSprite : defaultSprite;
        defenceLevel3.sprite = _equipment.defencePoints == 3 ? currentLevelSprite : defaultSprite;
        defenceLevel4.sprite = _equipment.defencePoints == 4 ? currentLevelSprite : defaultSprite;
        defenceLevel5.sprite = _equipment.defencePoints == 5 ? currentLevelSprite : defaultSprite;
    }

    public void UpgradeDefenceClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanWarriorEquipment, HumanWarriorEquipmentDto>(_equipment.id,
            UnitType.HumanWarrior,
            HumanWarriorEquipment.DefenceParamName, UpdateEquipment, MapDto);
    }

    private static HumanWarriorEquipment MapDto(HumanWarriorEquipmentDto dto)
    {
        return (HumanWarriorEquipment)dto.ToDomain();
    }

    private void UpdateEquipment(object sender, Equipment e)
    {
        SetEquipment(e);
    }

    public void UpgradeOffenceClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanWarriorEquipment, HumanWarriorEquipmentDto>(_equipment.id,
            UnitType.HumanWarrior,
            HumanWarriorEquipment.OffenceParamName, UpdateEquipment, MapDto);
    }

    public void SetEquipment(Equipment e)
    {
        _equipment = (HumanWarriorEquipment)e;
        ProcessButtons();
    }
}