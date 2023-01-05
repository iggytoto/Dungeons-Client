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
        if (unitStateController == null) return;
        if (slider == null) return;
        slider.maxValue = unitStateController.MaxHp;
        slider.minValue = 0;
        slider.value = unitStateController.HitPoints;
        if (unitStateController.IsDead())
        {
            gameObject.SetActive(false);
        }

        if (nameText == null) return;
        nameText.text = unitStateController.Name;
    }
}