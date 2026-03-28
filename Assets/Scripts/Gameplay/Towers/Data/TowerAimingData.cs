using CarXTowerDefence.Gameplay.Data;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Data
{
    public class TowerAimingData : TowerComponentData
    {
        [field: SerializeField][field: Range(0, 90)] public float rotationSpeedPerSeconds { get; private set; }
    }
}