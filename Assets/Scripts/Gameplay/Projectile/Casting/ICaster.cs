using CarXTowerDefence.Gameplay.Combat;
using CarXTowerDefence.Gameplay.Projectiles.Configs;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Projectiles.Casting
{
    public interface ICaster 
    {
        public ICaster CreateInstance();

        public void SetCasterTransform(Transform casterTransform);

        public void Cast(ProjectileConfig config, ITargetable target);
    }
}