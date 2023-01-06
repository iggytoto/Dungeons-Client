using System;
using Model.Units;
using TMPro;
using UnityEngine;

public class HorizontalUnitListButtonController : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text costText;
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
            nameText.text = $"{unit.Name}";
            if (unit.activity != UnitActivity.Idle)
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
                costText.text = $"{sale.GoldPrice.ToString()} Gold";
            }
            else
            {
                costText.gameObject.SetActive(false);
            }
        }
        else
        {
            nameText.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
            activityPanel.SetActive(false);
            costText.text = null;
        }
    }

    public void OnButtonClick()
    {
        ButtonClicked?.Invoke(this, _unit);
    }
}