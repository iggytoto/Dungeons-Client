using System;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace
{
    //todo make it monobehavior and load resources on start
    public abstract class ResourcesManager
    {
        public static GameObject LoadUnitPrefabForUnitType(UnitType type)
        {
            return Resources.Load<GameObject>(GetUnitPrefabResourcePath(type));
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