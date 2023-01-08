using System;
using System.Collections;
using System.Collections.Generic;
using Model.Units;
using Services;
using UnityEngine;

public class BuyUnitPopupUiController : MonoBehaviour
{
    private ITavernService _tavernService;

    private void Start()
    {
        _tavernService = FindObjectOfType<GameService>().TavernService;
    }

    public void OnMouseExit()
    {
        gameObject.SetActive(false);
    }

    private void BuyUnitButtonClicked(UnitType type)
    {
        _tavernService.BuyUnit(type);
    }

    public void BuyUnitButtonClicked(string type)
    {
        BuyUnitButtonClicked(Enum.Parse<UnitType>(type));
    }
}