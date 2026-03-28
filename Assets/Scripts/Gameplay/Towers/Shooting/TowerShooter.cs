using CarXTowerDefence.Gameplay.Combat;
using CarXTowerDefence.Gameplay.Projectiles.Casting;
using CarXTowerDefence.Gameplay.Projectiles.Configs;
using CarXTowerDefence.Gameplay.Towers.Data;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Shooting
{
    public sealed class TowerShooter : IShooter
    {
        private ICaster m_projectileCaster;
        private ProjectileConfig m_config;

        public TowerShooter(TowerShooterData data, ICaster caster)
        {
            m_projectileCaster = caster;
            m_config = data.projectileConfig;
        }

        public void SetShootPoint(Transform point)
        {
            if (m_projectileCaster is null)
            {
                throw new System.NullReferenceException("ProjectileCaster was not connected");
            }

            m_projectileCaster.SetCasterTransform(point);
        }

        public void TryShoot(ITargetable target)
        {
            if(m_config is null)
            {
                return;
            }

            m_projectileCaster.Cast(m_config, target);
        }
    }
}