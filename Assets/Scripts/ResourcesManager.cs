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
        private static readonly Dictionary<UnitType, Sprite> UnitTypeIcons1To2 = new();
        private static readonly Dictionary<UnitType, Sprite> UnitTypeIcons1To1 = new();
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
            if (!UnitTypeIcons1To1.ContainsKey(unitType))
            {
                UnitTypeIcons1To1.Add(unitType, Resources.Load<Sprite>(GetUnitIcon1To1ResourcePath(unitType)));
            }

            return UnitTypeIcons1To1[unitType];
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
            if (!UnitTypeIcons1To2.ContainsKey(unitType.Value))
            {
                UnitTypeIcons1To2.Add(unitType.Value, Resources.Load<Sprite>(GetUnitIcon1To2ResourcePath(unitType.Value)));
            }

            return UnitTypeIcons1To2[unitType.Value];
        }

        private static string GetUnitIcon1To2ResourcePath(UnitType unitType)
        {
            return unitType switch
            {
                UnitType.HumanArcher => "Images/Characters/HumanArcherImage1to2",
                UnitType.HumanCleric => "Images/Characters/HumanClericImage1to2",
                UnitType.HumanSpearman => "Images/Characters/HumanSpearmanImage1to2",
                UnitType.HumanWarrior => "Images/Characters/HumanWarriorImage1to2",
                _ => null
            };
        }

        private static string GetUnitIcon1To1ResourcePath(UnitType unitType)
        {
            return unitType switch
            {
                UnitType.HumanArcher => "Images/Characters/HumanArcherImage1to1",
                UnitType.HumanCleric => "Images/Characters/HumanClericImage1to1",
                UnitType.HumanSpearman => "Images/Characters/HumanSpearmanImage1to1",
                UnitType.HumanWarrior => "Images/Characters/HumanWarriorImage1to1",
                _ => null
            };
        }
    }
}