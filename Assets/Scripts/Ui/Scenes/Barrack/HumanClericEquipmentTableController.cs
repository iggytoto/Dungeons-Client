using DefaultNamespace;
using Model.Units;
using Services;
using Services.Common.Dto;
using Services.Dto;
using UnityEngine;
using UnityEngine.UI;

public class HumanClericEquipmentTableController : MonoBehaviour, IEquipmentTableController
{
    [SerializeField] private Button upgradeDisciplineButton;
    [SerializeField] private Button upgradeShatterButton;
    [SerializeField] private Button upgradeDivineButton;
    [SerializeField] private Button upgradePurgeButton;

    [SerializeField] private Sprite currentLevelSprite;
    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private Image disciplineLevel1;
    [SerializeField] private Image disciplineLevel2;
    [SerializeField] private Image disciplineLevel3;
    [SerializeField] private Image disciplineLevel4;
    [SerializeField] private Image disciplineLevel5;
    [SerializeField] private Image shatterLevel1;
    [SerializeField] private Image divineLevel1;
    [SerializeField] private Image purgeLevel1;

    private HumanClericEquipment _equipment;
    private IBarrackService _barrackService;

    private void Awake()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
    }

    private void ProcessButtons()
    {
        upgradeDisciplineButton.enabled = _equipment.disciplinePoints < 5;
        upgradeShatterButton.enabled = !_equipment.shatter;
        upgradeDivineButton.enabled = !_equipment.divine;
        upgradePurgeButton.enabled = !_equipment.purge;

        disciplineLevel1.sprite = _equipment.disciplinePoints == 1 ? currentLevelSprite : defaultSprite;
        disciplineLevel2.sprite = _equipment.disciplinePoints == 2 ? currentLevelSprite : defaultSprite;
        disciplineLevel3.sprite = _equipment.disciplinePoints == 3 ? currentLevelSprite : defaultSprite;
        disciplineLevel4.sprite = _equipment.disciplinePoints == 4 ? currentLevelSprite : defaultSprite;
        disciplineLevel5.sprite = _equipment.disciplinePoints == 5 ? currentLevelSprite : defaultSprite;
        shatterLevel1.sprite = _equipment.shatter ? currentLevelSprite : defaultSprite;
        divineLevel1.sprite = _equipment.divine ? currentLevelSprite : defaultSprite;
        purgeLevel1.sprite = _equipment.purge ? currentLevelSprite : defaultSprite;
    }

    public void UpgradeDisciplineClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanClericEquipment, HumanClericEquipmentDto>(_equipment.id,
            UnitType.HumanCleric,
            HumanClericEquipment.Discipline, UpdateEquipment, MapDto);
    }

    public void UpgradeShatterClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanClericEquipment, HumanClericEquipmentDto>(_equipment.id,
            UnitType.HumanCleric,
            HumanClericEquipment.Shatter, UpdateEquipment, MapDto);
    }

    public void UpgradeDivineClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanClericEquipment, HumanClericEquipmentDto>(_equipment.id,
            UnitType.HumanCleric,
            HumanClericEquipment.Divine, UpdateEquipment, MapDto);
    }

    public void UpgradePurgeClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanClericEquipment, HumanClericEquipmentDto>(_equipment.id,
            UnitType.HumanCleric,
            HumanClericEquipment.Shatter, UpdateEquipment, MapDto);
    }

    private static HumanClericEquipment MapDto(HumanClericEquipmentDto dto)
    {
        return (HumanClericEquipment)dto.ToDomain();
    }

    private void UpdateEquipment(object sender, Equipment e)
    {
        SetEquipment(e);
    }

    public void SetEquipment(Equipment e)
    {
        _equipment = (HumanClericEquipment)e;
        ProcessButtons();
    }
}