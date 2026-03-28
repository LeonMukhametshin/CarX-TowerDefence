using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Aiming
{
    public sealed class TowerAimer : IAimer
    {
        private Transform m_barrelTransform;
        private float m_rotationSpeedPerSeconds;

        public TowerAimer(float rotationSpeedPerSeconds)
        {
            m_rotationSpeedPerSeconds = rotationSpeedPerSeconds;
        }

        public void SetBarrel(Transform barrelTransfrom) =>
             m_barrelTransform = barrelTransfrom;

        public void RotateTowards(Vector3 target, Action callBack = null)
        {
            Vector3 fromTo = target - m_barrelTransform.position;

            m_barrelTransform.rotation = Quaternion.LookRotation(fromTo, Vector3.up);

            callBack?.Invoke();
        }
    }
}