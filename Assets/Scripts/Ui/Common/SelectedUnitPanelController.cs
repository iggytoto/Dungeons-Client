using System.Globalization;
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
    [SerializeField] public TMP_Text asText;
    [SerializeField] public TMP_Text arText;
    [SerializeField] public TMP_Text msText;
    [SerializeField] public TMP_Text bbText;
    [SerializeField] public TMP_Text acText;


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
            hpText.text = u.hitPoints.ToString();
            damageText.text = u.damage.ToString();
            armorText.text = u.armor.ToString();
            mrText.text = u.magicResistance.ToString();
            asText.text = u.attackSpeed.ToString(CultureInfo.InvariantCulture);
            arText.text = u.attackRange.ToString(CultureInfo.InvariantCulture);
            msText.text = u.movementSpeed.ToString(CultureInfo.InvariantCulture);
            bbText.text = u.battleBehavior.ToString();
            acText.text = u.activity.ToString();
        }
    }

    private void Reset()
    {
        nameText.text = "";
        hpText.text = "";
        damageText.text = "";
        armorText.text = "";
        mrText.text = "";
        asText.text = "";
        arText.text = "";
        msText.text = "";
        bbText.text = "";
        acText.text = "";
    }
}