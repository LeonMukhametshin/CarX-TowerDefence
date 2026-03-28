using CarXTowerDefence.Gameplay.Combat;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers
{
    public interface ITargeting
    {
        public void SetCurrentPosition(Vector3 position);

        public void UpdateTargets();

        public ITargetable GetBestTarget();
    }
}