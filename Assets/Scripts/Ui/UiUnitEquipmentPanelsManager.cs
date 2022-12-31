using System;
using Model.Units;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Services Configuration",
    menuName = "Scriptables/UI/UnitEquipmentPanelsManager")]
public class UiUnitEquipmentPanelsManager : ScriptableObject
{
    [SerializeField] private GameObject humanWarriorEquipmentTablePrefab;

    public GameObject GetEquipmentTablePrefabForType(Type t)
    {
        if (t == typeof(HumanWarriorEquipment))
        {
            return humanWarriorEquipmentTablePrefab;
        }

        return null;
    }
}