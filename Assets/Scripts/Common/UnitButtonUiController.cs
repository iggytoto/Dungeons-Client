using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text hpUnitText;
    [SerializeField] private TMP_Text maxHpUnitText;
    [SerializeField] private TMP_Text armorUnitText;
    [SerializeField] private TMP_Text mrUnitText;
    [SerializeField] private TMP_Text damageUnitText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image image;

    public event EventHandler<Unit> OnClick;

    private Unit _unit;

    public Unit Unit
    {
        get => _unit;
        set => OnSetUnit(value);
    }

    public void OnClicked()
    {
        OnClick?.Invoke(this, _unit);
    }

    private void OnSetUnit(Unit value)
    {
        if (value == null) return;
        _unit = value;
        if (hpUnitText != null)
            hpUnitText.text = value.hitPoints.ToString();
        if (maxHpUnitText != null)
            maxHpUnitText.text = value.maxHp.ToString();
        if (armorUnitText != null)
            armorUnitText.text = value.armor.ToString();
        if (mrUnitText != null)
            mrUnitText.text = value.magicResistance.ToString();
        if (damageUnitText != null)
            damageUnitText.text = value.damage.ToString();
        if (nameText != null)
            nameText.text = value.Name;
        if (image != null)
            image.sprite = ResourcesManager.GetInstance().LoadIcon200X200ForUnit(value.type);
    }
}