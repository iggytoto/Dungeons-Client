using System;
using Model.Units;
using Services;
using UnityEngine;

public class BuyUnitPopupUiController : MonoBehaviour
{
    private ITavernService _tavernService;

    public event Action<Unit> UnitPurchased;

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
        _tavernService.BuyUnit(type, OnUnitPurchased, OnError);
    }

    private void OnUnitPurchased(Unit obj)
    {
        UnitPurchased?.Invoke(obj);
    }

    private void OnError(string obj)
    {
        Debug.LogError(obj);
    }

    public void BuyUnitButtonClicked(string type)
    {
        BuyUnitButtonClicked(Enum.Parse<UnitType>(type));
    }
}