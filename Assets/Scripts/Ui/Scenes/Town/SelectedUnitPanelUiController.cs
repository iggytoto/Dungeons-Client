using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Model.Items;
using Model.Units;
using Services;
using TMPro;
using UnityEngine;

public class SelectedUnitPanelUiController : MonoBehaviour
{
    [SerializeField] private GameObject unitItemButtonPrefab;
    [SerializeField] private GameObject itemsContent;

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
    private readonly List<ItemButtonUiController> _itemButtonControllers = new();

    public void RemoveItem(Item i)
    {
        var bc = _itemButtonControllers.FirstOrDefault(x => x.Item.Id == i.Id);
        if (bc == null) return;
        Destroy(bc.gameObject);
        _itemButtonControllers.Remove(bc);
        UpdateView();
    }

    public void AddItem(Item item)
    {
        if (_itemButtonControllers.Any(x => x.Item.Id == item.Id)) return;
        var button = Instantiate(unitItemButtonPrefab, itemsContent.transform);
        var buttonController = button.GetComponent<ItemButtonUiController>();
        buttonController.OnClick += (i) => OnItemClicked?.Invoke(i);
        buttonController.Item = item;
        _itemButtonControllers.Add(buttonController);
        UpdateView();
    }


    public event Action<Item> OnItemClicked;

    public Unit Unit
    {
        get => _unit;
        set => SetUnit(value);
    }

    private void UpdateView()
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
        ClearItems();
        gameObject.SetActive(value != null);
        _unit = value;
        if (_unit != null)
        {
            foreach (var item in _unit.Items)
            {
                AddItem(item);
            }
        }
        UpdateView();
    }

    private void ClearItems()
    {
        foreach (var unitItem in _itemButtonControllers.ToArray())
        {
            RemoveItem(unitItem.Item);
        }
    }
}