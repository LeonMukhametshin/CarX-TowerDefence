using CarXTowerDefence.Gameplay.Data;
using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.Data
{
    [Serializable]
    public class TowerPrefabData : TowerComponentData
    {
        [field: SerializeField] public TowerController tower { get; private set; }
    }
}