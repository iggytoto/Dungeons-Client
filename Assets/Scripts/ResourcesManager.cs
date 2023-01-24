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
        private static readonly Dictionary<UnitType, Sprite> UnitTypeIcons150X300 = new();
        private static readonly Dictionary<UnitType, Sprite> UnitTypeIcons200X200 = new();
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

        public Sprite LoadIcon200X200ForUnit(UnitType unitType)
        {
            if (!UnitTypeIcons200X200.ContainsKey(unitType))
            {
                UnitTypeIcons200X200.Add(unitType, Resources.Load<Sprite>(GetUnitIcon200X200ResourcePath(unitType)));
            }

            return UnitTypeIcons200X200[unitType];
        }

        public Sprite GetIconForItem(ItemType t)
        {
            return t switch
            {
                _ => null
            };
        }

        public Sprite LoadIcon150X300ForUnit(UnitType? unitType)
        {
            if (unitType == null) return null;
            if (!UnitTypeIcons150X300.ContainsKey(unitType.Value))
            {
                UnitTypeIcons150X300.Add(unitType.Value, Resources.Load<Sprite>(GetUnitIcon150X300ResourcePath(unitType.Value)));
            }

            return UnitTypeIcons150X300[unitType.Value];
        }

        private static string GetUnitIcon150X300ResourcePath(UnitType unitType)
        {
            return unitType switch
            {
                UnitType.HumanArcher => "Images/HumanArcherImage150x300",
                _ => null
            };
        }

        private static string GetUnitIcon200X200ResourcePath(UnitType unitType)
        {
            return unitType switch
            {
                UnitType.HumanArcher => "Images/HumanArcherImage200x200",
                _ => null
            };
        }
    }
}