using System;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class ResourcesManager
    {
        public static GameObject LoadPrefabForUnitType(UnitType type)
        {
            return Resources.Load<GameObject>(GetPrefab(type));
        }

        private static string GetPrefab(UnitType type)
        {
            return type switch
            {
                UnitType.HumanWarrior => "Characters/HumanWarrior/HumanWarriorPrefab",
                UnitType.Dummy => "Characters/Dummy/DummyUnitPrefab",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}