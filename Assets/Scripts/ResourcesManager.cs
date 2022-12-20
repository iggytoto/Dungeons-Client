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
            return "Characters/Dummy/DummyUnitPrefab";
        }
    }
}