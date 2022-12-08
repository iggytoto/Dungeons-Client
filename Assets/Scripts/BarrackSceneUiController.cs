using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrackSceneUiController : MonoBehaviour
{
    [SerializeField] public Unit selectedUnit;
    [SerializeField] public Button trainUnitButton;
    private BarracksService _barracksService;

    private void Start()
    {
        trainUnitButton.enabled = false;
        trainUnitButton.onClick.AddListener(OnTrainButtonClicked);
        _barracksService = FindObjectOfType<BarracksService>();
    }

    private void OnTrainButtonClicked()
    {
        if (selectedUnit != null)
        {
            _barracksService.TrainUnit(selectedUnit.Id);
        }
    }


    public void SetSelectedUnit(Unit u)
    {
        selectedUnit = u;
        trainUnitButton.enabled = u is { Activity: Unit.UnitActivity.Idle };
    }
}