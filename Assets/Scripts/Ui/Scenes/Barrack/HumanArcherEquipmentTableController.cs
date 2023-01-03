using DefaultNamespace;
using Model.Units;
using Services;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

public class HumanArcherEquipmentTableController : MonoBehaviour, IEquipmentTableController
{
    [SerializeField] private Button upgradeMidRangeButton;
    [SerializeField] private Button upgradeLongRangeButton;
    [SerializeField] private Button upgradeFireArrowsButton;
    [SerializeField] private Button upgradePoisonArrowsButton;

    [SerializeField] private Sprite currentLevelSprite;
    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private Image midRangeLevel1;
    [SerializeField] private Image midRangeLevel2;
    [SerializeField] private Image midRangeLevel3;
    [SerializeField] private Image midRangeLevel4;
    [SerializeField] private Image longRangeLevel1;
    [SerializeField] private Image longRangeLevel2;
    [SerializeField] private Image longRangeLevel3;
    [SerializeField] private Image longRangeLevel4;
    [SerializeField] private Image fireArrows;
    [SerializeField] private Image poisonArrows;

    private HumanArcherEquipment _equipment;
    private IBarrackService _barrackService;

    private void Awake()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
    }

    private void ProcessButtons()
    {
        upgradeMidRangeButton.enabled = _equipment.midRangePoints < 4;
        upgradeLongRangeButton.enabled = _equipment.longRangePoints < 4;
        upgradeFireArrowsButton.enabled = !_equipment.fireArrows;
        upgradePoisonArrowsButton.enabled = !_equipment.poisonArrows;

        midRangeLevel1.sprite = _equipment.midRangePoints == 1 ? currentLevelSprite : defaultSprite;
        midRangeLevel2.sprite = _equipment.midRangePoints == 2 ? currentLevelSprite : defaultSprite;
        midRangeLevel3.sprite = _equipment.midRangePoints == 3 ? currentLevelSprite : defaultSprite;
        midRangeLevel4.sprite = _equipment.midRangePoints == 4 ? currentLevelSprite : defaultSprite;
        longRangeLevel1.sprite = _equipment.longRangePoints == 1 ? currentLevelSprite : defaultSprite;
        longRangeLevel2.sprite = _equipment.longRangePoints == 2 ? currentLevelSprite : defaultSprite;
        longRangeLevel3.sprite = _equipment.longRangePoints == 3 ? currentLevelSprite : defaultSprite;
        longRangeLevel4.sprite = _equipment.longRangePoints == 4 ? currentLevelSprite : defaultSprite;

        fireArrows.sprite = _equipment.fireArrows ? currentLevelSprite : defaultSprite;
        poisonArrows.sprite = _equipment.poisonArrows ? currentLevelSprite : defaultSprite;
    }

    private static HumanArcherEquipment MapDto(HumanArcherEquipmentDto dto)
    {
        return (HumanArcherEquipment)dto.ToDomain();
    }

    public void UpgradeMidRangeClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanArcherEquipment, HumanArcherEquipmentDto>(_equipment.id,
            UnitType.HumanArcher,
            HumanArcherEquipment.MidRangeParamName, UpdateEquipment, MapDto);
    }

    private void UpdateEquipment(object sender, HumanArcherEquipment e)
    {
        SetEquipment(e);
    }

    public void UpgradeLongRangeClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanArcherEquipment, HumanArcherEquipmentDto>(_equipment.id,
            UnitType.HumanArcher,
            HumanArcherEquipment.LongRangeParamName, UpdateEquipment, MapDto);
    }

    public void UpgradeFireArrowsClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanArcherEquipment, HumanArcherEquipmentDto>(_equipment.id,
            UnitType.HumanArcher,
            HumanArcherEquipment.FireArrowsParamName, UpdateEquipment, MapDto);
    }

    public void UpgradePoisonArrowsClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanArcherEquipment, HumanArcherEquipmentDto>(_equipment.id,
            UnitType.HumanArcher,
            HumanArcherEquipment.PoisonArrowsParamName, UpdateEquipment, MapDto);
    }

    public void SetEquipment(Equipment e)
    {
        _equipment = (HumanArcherEquipment)e;
        ProcessButtons();
    }
}