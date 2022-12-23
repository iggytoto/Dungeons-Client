using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class AttackClosestEnemyBattleBehaviour : NavMeshAgentBasedBattleBehaviour, IBattleBehaviour
    {
        private readonly List<UnitController> _availableTargets = new();
        private const float PathUpdateInterval = 1;
        private float _pathUpdateTimer;

        private new void Update()
        {
            if (UnitController.Unit.IsDead()) return;
            base.Update();
            _pathUpdateTimer -= Time.deltaTime;
            Target = GetClosestTarget();
            if (Target == null)
            {
                UnitController.Stop();
            }

            if (CanAttack())
            {
                UnitController.Attack(Target.transform.position, 1 / UnitController.Unit.AttackSpeed);
                Target.Unit.DoDamage(UnitController.Unit.Damage);
                _attackCooldown = 1 / UnitController.Unit.AttackSpeed;
            }
            else
            {
                if (!(_pathUpdateTimer <= 0) || Target == null) return;
                SetPathTo(Target.transform.position);
                _pathUpdateTimer = PathUpdateInterval;
            }
        }

        private UnitController GetClosestTarget()
        {
            return GetTargets()
                .Where(uc => !uc.Unit.IsDead())
                .OrderBy(uc => Vector3.Distance(UnitController.transform.position, uc.gameObject.transform.position))
                .FirstOrDefault();
        }

        private IEnumerable<UnitController> GetTargets()
        {
            if (!_availableTargets.Any())
            {
                _availableTargets.AddRange(FindObjectsOfType<UnitController>()
                    .Where(uc => uc.Unit.OwnerId != UnitController.Unit.OwnerId));
            }

            return _availableTargets;
        }
    }
}