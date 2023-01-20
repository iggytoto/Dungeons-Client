using UnityEngine;
using UnityEngine.UI;

public class UnitStateBarUiController : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private GameObject effectsContent;
    private UnitStateController _unitStateController;
    private Transform _camTransform;


    private void Start()
    {
        _unitStateController = gameObject.GetComponentInParent<UnitStateController>();
        _camTransform = FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        if (_unitStateController == null) return;
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)_unitStateController.HitPoints / _unitStateController.MaxHp;
        }

        if (manaBar != null)
        {
            manaBar.fillAmount = (float)_unitStateController.Mana / _unitStateController.MaxMana;
        }

        transform.rotation = Quaternion.LookRotation(transform.position - _camTransform.position);
    }
}