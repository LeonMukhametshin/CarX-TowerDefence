using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Aiming
{
    public interface IAimer
    {
        public void SetBarrel(Transform point);

        public void RotateTowards(Vector3 target, Action callBack = null);
    }
}