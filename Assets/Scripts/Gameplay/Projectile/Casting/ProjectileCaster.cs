using CarXTowerDefence.Gameplay.Combat;
using CarXTowerDefence.Gameplay.Projectiles.Configs;
using CarXTowerDefence.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Projectiles.Casting
{
    public sealed class ProjectileCaster : ICaster
    {
        private const int initialPoolSize = 5;

        private readonly Dictionary<GameObject, GameObjectPool> m_pools = new();

        private Transform m_casterTransform;

        public ICaster CreateInstance() =>
            new ProjectileCaster();

        public void SetCasterTransform(Transform casterTransform) =>
            m_casterTransform = casterTransform;

        public void Cast(ProjectileConfig config, ITargetable target)
        {
            if (config.view is null)
            {
                throw new NullReferenceException("Projectile prefab is null");
            }

            var pool = GetPool(config.view);
            var projectileGameObject = pool.Get();

            projectileGameObject.transform.SetPositionAndRotation(
                m_casterTransform.position,
                m_casterTransform.rotation);

            SetLayer(projectileGameObject);

            var velocity = m_casterTransform.forward * config.speed;

            if (!projectileGameObject.TryGetComponent(out Projectile projectile))
            {
                throw new InvalidOperationException(
                    $"Prefab '{config.view.name}' must contain {nameof(Projectile)}");
            }

            projectile.SetPool(pool);
            projectile.Initialize(
                m_casterTransform.position,
                velocity,
                target,
                config.effects);
        }

        private GameObjectPool GetPool(GameObject prefab)
        {
            if (!m_pools.TryGetValue(prefab, out var pool))
            {
                pool = new GameObjectPool(prefab, initialPoolSize);
                m_pools.Add(prefab, pool);
            }

            return pool;
        }

        private void SetLayer(GameObject targetObject) =>
            targetObject.layer = m_casterTransform.gameObject.layer;
    }
}