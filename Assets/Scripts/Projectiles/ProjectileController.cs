using System;
using Model.Damage;
using UnityEngine;

namespace DefaultNamespace.Projectiles
{
    public class ProjectileController : MonoBehaviour
    {
        private UnitStateController _target;
        private Damage _damage;
        private const float Speed = 10;
        private const float SelfDestructTime = 60;
        private float _selfDestructTimer;
        private Action<UnitStateController> _onProjectileEffect;

        public void Init(UnitStateController source, UnitStateController target, Damage damage,
            Action<UnitStateController> onProjectileHitHandler = null)
        {
            gameObject.transform.position = new Vector3(source.transform.position.x, 1.5f, source.transform.position.z);
            _target = target;
            _damage = damage;
            _onProjectileEffect = onProjectileHitHandler;
        }

        private void Update()
        {
            _selfDestructTimer += Time.deltaTime;
            if (_selfDestructTimer >= SelfDestructTime)
            {
                Destroy(gameObject);
            }

            if (_target == null) return;
            var destination = _target.transform.position;
            var currentPosition = gameObject.transform.position;
            if (Vector3.Distance(currentPosition, destination) <= .01)
            {
                _target.DoDamage(_damage);
                _onProjectileEffect?.Invoke(_target);
                Destroy(gameObject);
            }

            gameObject.transform.position =
                Vector3.MoveTowards(currentPosition, destination,
                    Speed * Time.deltaTime);
            gameObject.transform.LookAt(destination);
        }
    }
}