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
            nameText.text = $"{unit.Name}";
            hpText.text = $"HP:{unit.HitPoints.ToString()}";
            armorText.text = $"ARM:{unit.Armor.ToString()}";
            mrText.text = $"MR:{unit.MagicResistance.ToString()}";
            trainingExpText.text = $"TE:{unit.TrainingExperience.ToString()}";
            if (unit is UnitForSale sale)
            {
                costText.gameObject.SetActive(true);
                costText.text = $"COST:{sale.GoldPrice.ToString()}";
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
            hpText.text = null;
            armorText.text = null;
            mrText.text = null;
            trainingExpText.text = null;
            costText.text = null;
        }
    }

    public void OnButtonClick()
    {
        ButtonClicked?.Invoke(this, _unit);
    }
}