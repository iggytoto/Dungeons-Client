using TMPro;
using UnityEngine;

public class UnitButtonUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text hpUnitText;
    [SerializeField] private TMP_Text maxHpUnitText;
    [SerializeField] private TMP_Text armorUnitText;
    [SerializeField] private TMP_Text mrUnitText;
    [SerializeField] private TMP_Text damageUnitText;
    [SerializeField] private TMP_Text nameText;
    private Unit _unit;

    public Unit Unit
    {
        get => _unit;
        set => OnSetUnit(value);
    }

    private void OnSetUnit(Unit value)
    {
        if (value == null) return;
        _unit = value;
        hpUnitText.text = value.hitPoints.ToString();
        maxHpUnitText.text = value.maxHp.ToString();
        armorUnitText.text = value.armor.ToString();
        mrUnitText.text = value.magicResistance.ToString();
        damageUnitText.text = value.damage.ToString();
        nameText.text = value.Name;
    }
}