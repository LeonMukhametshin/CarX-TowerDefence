using UnityEngine;

namespace CarXTowerDefence.Gameplay.Combat
{
    public interface ITargetable : IDamageReservable
    {
        public Vector3 position { get; }
        public Vector3 velocity { get; }
        public bool isTargetable { get; }
    }
}