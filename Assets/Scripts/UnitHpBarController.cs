using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpBarController : MonoBehaviour
{
    [SerializeField] public UnitController unitController;
    [SerializeField] public Slider slider;
    [SerializeField] public TMP_Text nameText;

    private void Update()
    {
        if (unitController == null || unitController.Unit == null) return;
        if (slider == null) return;
        slider.maxValue = unitController.Unit.maxHp;
        slider.minValue = 0;
        slider.value = unitController.Unit.hitPoints;
        if (unitController.Unit.IsDead())
        {
            gameObject.SetActive(false);
        }

        if (nameText == null) return;
        nameText.text = unitController.Unit.Name;
    }
}