using CarXTowerDefence.Gameplay.Data;
using CarXTowerDefence.Gameplay.Projectiles.Casting;
using CarXTowerDefence.Gameplay.Projectiles.Configs;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Data
{
    public class TowerShooterData : TowerComponentData
    {
        [field: SerializeReference]
        [field: SerializeReferenceDropdown]
        public ICaster caster { get; private set; }

        [field: SerializeField] public ProjectileConfig projectileConfig { get; private set; }

        [field: SerializeField][field: Min(0)] public float shootInterval { get; private set; }
    }
}