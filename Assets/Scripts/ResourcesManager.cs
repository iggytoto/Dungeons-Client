using System;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourcesManager : MonoBehaviour
    {
        private readonly Dictionary<UnitType, GameObject> _unitTypePrefabs = new();


        public GameObject LoadUnitPrefabForUnitType(UnitType type)
        {
            if (!_unitTypePrefabs.ContainsKey(type))
            {
                _unitTypePrefabs.Add(type, Resources.Load<GameObject>(GetUnitPrefabResourcePath(type)));
            }

            return _unitTypePrefabs[type];
        }

        private static string GetUnitPrefabResourcePath(UnitType type)
        {
            return type switch
            {
                UnitType.HumanWarrior => "Characters/HumanWarrior/HumanWarriorPrefab",
                UnitType.Dummy => "Characters/Dummy/DummyUnitPrefab",
                UnitType.HumanArcher => "Characters/HumanArcher/HumanArcherPrefab",
                UnitType.HumanSpearman => "Characters/HumanSpearman/HumanSpearmanPrefab",
                UnitType.HumanCleric => "Characters/HumanCleric/HumanClericPrefab",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}