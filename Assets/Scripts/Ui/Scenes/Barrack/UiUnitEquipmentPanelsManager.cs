using System;
using Model.Units;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Services Configuration",
    menuName = "Scriptables/UI/UnitEquipmentPanelsManager")]
public class UiUnitEquipmentPanelsManager : ScriptableObject
{
    [SerializeField] private GameObject humanWarriorEquipmentTablePrefab;
    [SerializeField] private GameObject humanArcherEquipmentTablePrefab;
    [SerializeField] private GameObject humanSpearmanEquipmentTablePrefab;
    [SerializeField] private GameObject humanClericEquipmentTablePrefab;

    public GameObject GetEquipmentTablePrefabForType(Type t)
    {
        if (t == typeof(HumanWarriorEquipment))
        {
            return humanWarriorEquipmentTablePrefab;
        }
        else if (t == typeof(HumanArcherEquipment))
        {
            return humanArcherEquipmentTablePrefab;
        }
        else if (t == typeof(HumanSpearmanEquipment))
        {
            return humanSpearmanEquipmentTablePrefab;
        }
        else if (t == typeof(HumanClericEquipment))
        {
            return humanClericEquipmentTablePrefab;
        }

        return null;
    }
}