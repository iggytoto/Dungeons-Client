using System;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourcesManager : MonoBehaviour
    {
        private readonly Dictionary<UnitType, GameObject> _unitTypePrefabs = new();
        private readonly Dictionary<UnitType, GameObject> _unitTypeProjectiles = new();


        public GameObject LoadUnitPrefabForUnitType(UnitType type)
        {
            if (!_unitTypePrefabs.ContainsKey(type))
            {
                _unitTypePrefabs.Add(type, Resources.Load<GameObject>(GetUnitPrefabResourcePath(type)));
            }

            return _unitTypePrefabs[type];
        }

        public GameObject LoadProjectileForUnitType(UnitType type)
        {
            if (!_unitTypeProjectiles.ContainsKey(type))
            {
                _unitTypeProjectiles.Add(type, GetProjectileFor(type));
            }

            return _unitTypeProjectiles[type];
        }

        private GameObject GetProjectileFor(UnitType u)
        {
            return u switch
            {
                UnitType.HumanArcher => Resources.Load<GameObject>("Projectiles/Arrow"),
                UnitType.HumanCleric => Resources.Load<GameObject>("Projectiles/HolyBall"),
                _ => null
            };
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