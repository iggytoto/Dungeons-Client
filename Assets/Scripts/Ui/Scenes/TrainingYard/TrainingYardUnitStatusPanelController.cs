using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingYardUnitStatusPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text unitNameText;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider manaSlider;

    public Unit unit;

    private void Update()
    {
        if (unit == null) return;
        if (hpSlider != null)
        {
            hpSlider.maxValue = unit.maxHp;
            hpSlider.minValue = 0;
            hpSlider.value = unit.hitPoints;
        }

        if (manaSlider != null)
        {
            manaSlider.maxValue = unit.maxMana;
            manaSlider.minValue = 0;
            manaSlider.value = unit.mana;
        }


        if (unitNameText != null)
        {
            unitNameText.text = unit.Name;
        }
    }
}