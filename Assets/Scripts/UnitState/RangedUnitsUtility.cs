using System;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace.UnitState
{
    public abstract class RangedUnitsUtility
    {
        public static GameObject GetProjectileFor(Unit u)
        {
            return u.type switch
            {
                UnitType.HumanArcher => Resources.Load<GameObject>("Projectiles/Arrow"),
                UnitType.HumanCleric => Resources.Load<GameObject>("Projectiles/HolyBall"),
                _ => null
            };
        }
    }
}