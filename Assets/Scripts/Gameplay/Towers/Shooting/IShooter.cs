using CarXTowerDefence.Gameplay.Combat;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Shooting
{
    public interface IShooter
    {
        public void SetShootPoint(Transform point);
        public void TryShoot(ITargetable target);
    }
}