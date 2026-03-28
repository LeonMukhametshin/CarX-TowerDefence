using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Data
{
    [Serializable]
    public class TowerComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public TowerComponentData() 
        {
            SetComponentName();
        }

        public void SetComponentName() => name = GetType().Name;
    }
}