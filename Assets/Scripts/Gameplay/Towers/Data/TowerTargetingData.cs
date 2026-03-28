using CarXTowerDefence.Gameplay.Data;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Data
{
    public class TowerTargetingData : TowerComponentData
    {
        [field: SerializeField] public LayerMask targetLayer { get; private set; }
        [field: SerializeField][Min(0)] public float detectRadius { get; private set; }
    }
}