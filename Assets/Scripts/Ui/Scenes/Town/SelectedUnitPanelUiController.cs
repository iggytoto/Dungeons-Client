using System.Globalization;
using TMPro;
using UnityEngine;

public class SelectedUnitPanelUiController : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text maxHpText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text asText;
    [SerializeField] TMP_Text arText;
    [SerializeField] TMP_Text armText;
    [SerializeField] TMP_Text mrText;
    [SerializeField] TMP_Text msText;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Text bbText;


    public Unit Unit
    {
        get => _unit;
        set => SetUnit(value);
    }

    private void SetUnit(Unit value)
    {
        gameObject.SetActive(value != null);

        _unit = value;
        nameText.text = _unit.Name;
        hpText.text = _unit.hitPoints.ToString();
        maxHpText.text = _unit.maxHp.ToString();
        damageText.text = _unit.damage.ToString();
        asText.text = _unit.attackSpeed.ToString(CultureInfo.InvariantCulture);
        arText.text = _unit.attackRange.ToString(CultureInfo.InvariantCulture);
        armText.text = _unit.armor.ToString();
        mrText.text = _unit.magicResistance.ToString();
        msText.text = _unit.movementSpeed.ToString(CultureInfo.InvariantCulture);
        manaText.text = _unit.maxMana.ToString();
        bbText.text = _unit.battleBehavior.ToString();
    }


    private Unit _unit;
}