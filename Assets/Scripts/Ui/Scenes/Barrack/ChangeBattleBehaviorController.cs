using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Units;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChangeBattleBehaviorController : MonoBehaviour
{
    [SerializeField] public UnityEvent<BattleBehavior> OkClicked = new();
    [SerializeField] public TMP_Dropdown Dropdown;


    public void OnOkClicked()
    {
        OkClicked.Invoke(Enum.Parse<BattleBehavior>(Dropdown.options[Dropdown.value].text));
        gameObject.SetActive(false);
    }

    public void OnCancelClicked()
    {
        gameObject.SetActive(false);
    }

    public void ShowModal()
    {
        var values = Enum.GetValues(typeof(BattleBehavior));
        Dropdown.options = (from object value in values select new TMP_Dropdown.OptionData { text = value.ToString() })
            .ToList();
        gameObject.SetActive(true);
    }
}