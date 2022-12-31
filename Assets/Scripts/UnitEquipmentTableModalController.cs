using DefaultNamespace;
using Model.Units;
using UnityEngine;

public class UnitEquipmentTableModalController : MonoBehaviour
{
    [SerializeField] private UiUnitEquipmentPanelsManager panelsManager;
    private Equipment _equipment;
    private GameObject _currentTable;

    public void SetEquipment(Equipment e)
    {
        _equipment = e;
    }

    public void ShowModal()
    {
        gameObject.SetActive(true);
        if (_currentTable != null)
        {
            Destroy(_currentTable);
        }

        if (_equipment != null)
        {
            _currentTable = Instantiate(panelsManager.GetEquipmentTablePrefabForType(_equipment.GetType()),
                gameObject.transform);
            _currentTable.GetComponent<IEquipmentTableController>().SetEquipment(_equipment);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}