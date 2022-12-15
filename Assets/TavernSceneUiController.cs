using Services;
using UnityEngine;

public class TavernSceneUiController : MonoBehaviour
{
    private ITavernService _tavernService;

    private void Start()
    {
        _tavernService = FindObjectOfType<GameService>().TavernService;
    }


    public void OnTavernUnitClicked(Unit unit)
    {
        _tavernService.BuyUnit(unit);
    }
}