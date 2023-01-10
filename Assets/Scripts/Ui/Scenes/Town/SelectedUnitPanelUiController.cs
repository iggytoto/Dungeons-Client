using System;
using System.Globalization;
using System.Linq;
using Model.Units;
using Services;
using TMPro;
using UnityEngine;

public class SelectedUnitPanelUiController : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text maxHpText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text asText;
    [SerializeField] TMP_Text arText;
    [SerializeField] TMP_Text armText;
    [SerializeField] TMP_Text mrText;
    [SerializeField] TMP_Text msText;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Dropdown bbDropdown;
    [SerializeField] TMP_InputField nameInputField;

    private long _id;

    private void Start()
    {
        bbDropdown.onValueChanged.AddListener(OnBattleBehaviorChanged);
        _barrackService = FindObjectOfType<GameService>().BarrackService;
        nameInputField.onValueChanged.AddListener(OnNameChanged);
    }

    private void OnNameChanged(string arg0)
    {
        if (!_unit.Name.Equals(arg0))
        {
            _barrackService.ChangeUnitName(_id, nameInputField.text);
        }
    }

    private void OnBattleBehaviorChanged(int dropDownIndex)
    {
        _barrackService.ChangeUnitBattleBehavior(_id,
            Enum.Parse<BattleBehavior>(bbDropdown.options[bbDropdown.value].text));
        bbDropdown.transform.Find("Template").gameObject.SetActive(false);
    }

    public Unit Unit
    {
        get => _unit;
        set => SetUnit(value);
    }

    private void SetUnit(Unit value)
    {
        gameObject.SetActive(value != null);

        _unit = value;
        _id = _unit?.Id ?? 0;
        nameInputField.text = _unit?.Name;
        hpText.text = _unit?.hitPoints.ToString();
        maxHpText.text = _unit?.maxHp.ToString();
        damageText.text = _unit?.damage.ToString();
        asText.text = _unit?.attackSpeed.ToString(CultureInfo.InvariantCulture);
        arText.text = _unit?.attackRange.ToString(CultureInfo.InvariantCulture);
        armText.text = _unit?.armor.ToString();
        mrText.text = _unit?.magicResistance.ToString();
        msText.text = _unit?.movementSpeed.ToString(CultureInfo.InvariantCulture);
        manaText.text = _unit?.maxMana.ToString();
        var optionList = (from object bbValue in Enum.GetValues(typeof(BattleBehavior))
            select new TMP_Dropdown.OptionData(bbValue.ToString())).ToList();

        bbDropdown.options = optionList;
    }


    private Unit _unit;
    private IBarrackService _barrackService;
}