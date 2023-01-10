using System;
using System.Globalization;
using System.Linq;
using Model.Items;
using Model.Units;
using Services;
using TMPro;
using UnityEngine;

public class SelectedUnitPanelUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text maxHpText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text asText;
    [SerializeField] private TMP_Text arText;
    [SerializeField] private TMP_Text armText;
    [SerializeField] private TMP_Text mrText;
    [SerializeField] private TMP_Text msText;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Dropdown bbDropdown;
    [SerializeField] private TMP_InputField nameInputField;

    private Unit _unit;
    private IBarrackService _barrackService;

    public event Action<Item> OnItemClicked;

    public Unit Unit
    {
        get => _unit;
        set => SetUnit(value);
    }

    public void UpdateView()
    {
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
            _barrackService.ChangeUnitName(_unit.Id, nameInputField.text);
        }
    }

    private void OnBattleBehaviorChanged(int dropDownIndex)
    {
        _barrackService.ChangeUnitBattleBehavior(_unit.Id,
            Enum.Parse<BattleBehavior>(bbDropdown.options[bbDropdown.value].text));
        bbDropdown.transform.Find("Template").gameObject.SetActive(false);
    }

    private void SetUnit(Unit value)
    {
        gameObject.SetActive(value != null);
        _unit = value;
        UpdateView();
    }
}