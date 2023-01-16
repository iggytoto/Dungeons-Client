using System;
using System.Collections.Generic;
using Model.Items;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourcesManager : MonoBehaviour
    {
        private static readonly Dictionary<UnitType, GameObject> UnitTypePrefabs = new();
        private static readonly Dictionary<UnitType, GameObject> UnitTypeProjectiles = new();
        private static ResourcesManager _instance;

        private void Awake()
        {
            _instance = this;
        }

        public GameObject LoadUnitPrefabForUnitType(UnitType type)
        {
            if (!UnitTypePrefabs.ContainsKey(type))
            {
                UnitTypePrefabs.Add(type, Resources.Load<GameObject>(GetUnitPrefabResourcePath(type)));
            }

            return UnitTypePrefabs[type];
        }

        public GameObject LoadProjectileForUnitType(UnitType type)
        {
            if (!UnitTypeProjectiles.ContainsKey(type))
            {
                UnitTypeProjectiles.Add(type, GetProjectileFor(type));
            }

            return UnitTypeProjectiles[type];
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

        public static ResourcesManager GetInstance()
        {
            return _instance;
        }

        public Sprite GetIconForItem(ItemType t)
        {
            return t switch
            {
                _ => null
            };
        }
    }
}