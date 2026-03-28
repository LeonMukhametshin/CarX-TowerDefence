using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.AimStrategy
{
    public interface IAimStrategy
    {
        public bool TryGetAimPoint(
             Vector3 shooterPosition,
             Vector3 targetPosition,
             Vector3 targetVelocity,
             float projectileSpeed,
             out Vector3 aimPoint);
    }
}