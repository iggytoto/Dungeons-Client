using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpBarController : MonoBehaviour
{
    [SerializeField] public UnitStateController unitStateController;
    [SerializeField] public Slider slider;
    [SerializeField] public TMP_Text nameText;

    private void Update()
    {
        if (unitStateController == null || unitStateController.Unit == null) return;
        if (slider == null) return;
        slider.maxValue = unitStateController.Unit.maxHp;
        slider.minValue = 0;
        slider.value = unitStateController.Unit.hitPoints;
        if (unitStateController.IsDead())
        {
            gameObject.SetActive(false);
        }

        if (nameText == null) return;
        nameText.text = unitStateController.Unit.Name;
    }
}