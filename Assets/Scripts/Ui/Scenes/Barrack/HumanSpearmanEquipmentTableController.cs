using DefaultNamespace;
using Model.Units;
using Services;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.UI;

public class HumanSpearmanEquipmentTableController : MonoBehaviour, IEquipmentTableController
{
    [SerializeField] private Button upgradeDoubleEdgeButton;
    [SerializeField] private Button upgradeMidRangeButton;

    [SerializeField] private Sprite currentLevelSprite;
    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private Image doubleEdgeLevel1;
    [SerializeField] private Image doubleEdgeLevel2;
    [SerializeField] private Image doubleEdgeLevel3;
    [SerializeField] private Image doubleEdgeLevel4;
    [SerializeField] private Image doubleEdgeLevel5;
    [SerializeField] private Image midRangeLevel1;
    [SerializeField] private Image midRangeLevel2;
    [SerializeField] private Image midRangeLevel3;
    [SerializeField] private Image midRangeLevel4;
    [SerializeField] private Image midRangeLevel5;

    private HumanSpearmanEquipment _equipment;
    private IBarrackService _barrackService;

    private void Awake()
    {
        _barrackService = FindObjectOfType<GameService>().BarrackService;
    }

    private void ProcessButtons()
    {
        upgradeDoubleEdgeButton.enabled = _equipment.doubleEdgePoints < 5;
        upgradeMidRangeButton.enabled = _equipment.midRangePoints < 5;

        doubleEdgeLevel1.sprite = _equipment.doubleEdgePoints == 1 ? currentLevelSprite : defaultSprite;
        doubleEdgeLevel2.sprite = _equipment.doubleEdgePoints == 2 ? currentLevelSprite : defaultSprite;
        doubleEdgeLevel3.sprite = _equipment.doubleEdgePoints == 3 ? currentLevelSprite : defaultSprite;
        doubleEdgeLevel4.sprite = _equipment.doubleEdgePoints == 4 ? currentLevelSprite : defaultSprite;
        doubleEdgeLevel5.sprite = _equipment.doubleEdgePoints == 5 ? currentLevelSprite : defaultSprite;
        midRangeLevel1.sprite = _equipment.midRangePoints == 1 ? currentLevelSprite : defaultSprite;
        midRangeLevel2.sprite = _equipment.midRangePoints == 2 ? currentLevelSprite : defaultSprite;
        midRangeLevel3.sprite = _equipment.midRangePoints == 3 ? currentLevelSprite : defaultSprite;
        midRangeLevel4.sprite = _equipment.midRangePoints == 4 ? currentLevelSprite : defaultSprite;
        midRangeLevel5.sprite = _equipment.midRangePoints == 5 ? currentLevelSprite : defaultSprite;
    }

    public void UpgradeDoubleEdgeClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanSpearmanEquipment, HumanSpearmanEquipmentDto>(_equipment.id,
            UnitType.HumanWarrior,
            HumanSpearmanEquipment.DoubleEdgeParamName, UpdateEquipment, MapDto);
    }

    private static HumanSpearmanEquipment MapDto(HumanSpearmanEquipmentDto dto)
    {
        return (HumanSpearmanEquipment)dto.ToDomain();
    }

    private void UpdateEquipment(object sender, Equipment e)
    {
        SetEquipment(e);
    }

    public void UpgradeMidRangeClicked()
    {
        _barrackService.UpgradeUnitEquipment<HumanSpearmanEquipment, HumanSpearmanEquipmentDto>(_equipment.id,
            UnitType.HumanWarrior,
            HumanSpearmanEquipment.MidRangeParamName, UpdateEquipment, MapDto);
    }

    public void SetEquipment(Equipment e)
    {
        _equipment = (HumanSpearmanEquipment)e;
        ProcessButtons();
    }
}