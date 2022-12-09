using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitPanelController : MonoBehaviour
{
    [SerializeField] public TMP_Text nameText;
    [SerializeField] public Image image;
    [SerializeField] public TMP_Text hpText;
    [SerializeField] public TMP_Text damageText;
    [SerializeField] public TMP_Text armorText;
    [SerializeField] public TMP_Text mrText;
    [SerializeField] public TMP_Text teText;


    private void Start()
    {
        Reset();
    }

    public void SetSelectedUnit(Unit u)
    {
        if (u == null)
        {
            Reset();
        }
        else
        {
            nameText.text = u.Name;
            hpText.text = u.HitPoints.ToString();
            damageText.text = u.Damage.ToString();
            armorText.text = u.Armor.ToString();
            mrText.text = u.MagicResistance.ToString();
            teText.text = u.TrainingExperience.ToString();
        }
    }

    private void Reset()
    {
        nameText.text = "";
        hpText.text = "";
        damageText.text = "";
        armorText.text = "";
        mrText.text = "";
        teText.text = "";
    }
}