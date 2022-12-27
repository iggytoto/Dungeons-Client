using System;
using TMPro;
using UnityEngine;

public class HorizontalUnitListButtonController : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text hpText;
    public TMP_Text armorText;
    public TMP_Text mrText;
    public TMP_Text trainingExpText;
    public TMP_Text costText;
    public TMP_Text damageText;
    public TMP_Text activityText;
    public GameObject activityPanel;

    public event EventHandler<Unit> ButtonClicked;
    private Unit _unit;


    public void SetModel(Unit unit)
    {
        _unit = unit;
        if (unit != null)
        {
            nameText.gameObject.SetActive(true);
            hpText.gameObject.SetActive(true);
            armorText.gameObject.SetActive(true);
            mrText.gameObject.SetActive(true);
            trainingExpText.gameObject.SetActive(true);
            damageText.gameObject.SetActive(true);
            nameText.text = $"{unit.Name}";
            hpText.text = $"{unit.hitPoints.ToString()}";
            armorText.text = $"{unit.armor.ToString()}";
            mrText.text = $"{unit.magicResistance.ToString()}";
            trainingExpText.text = $"{unit.trainingExperience.ToString()}";
            damageText.text = $"{unit.damage.ToString()}";
            if (unit.activity != Unit.UnitActivity.Idle)
            {
                activityPanel.SetActive(true);
                activityText.text = unit.activity.ToString();
            }
            else
            {
                activityPanel.SetActive(false);
            }

            if (unit is UnitForSale sale)
            {
                costText.gameObject.SetActive(true);
                costText.text = $"{sale.GoldPrice.ToString()}";
            }
            else
            {
                costText.gameObject.SetActive(false);
            }
        }
        else
        {
            nameText.gameObject.SetActive(false);
            hpText.gameObject.SetActive(false);
            armorText.gameObject.SetActive(false);
            mrText.gameObject.SetActive(false);
            trainingExpText.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
            activityPanel.SetActive(false);
            hpText.text = null;
            armorText.text = null;
            mrText.text = null;
            trainingExpText.text = null;
            costText.text = null;
            damageText.text = null;
        }
    }

    public void OnButtonClick()
    {
        ButtonClicked?.Invoke(this, _unit);
    }
}