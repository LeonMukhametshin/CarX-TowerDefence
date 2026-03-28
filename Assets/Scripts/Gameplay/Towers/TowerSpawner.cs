using CarXTowerDefence.Gameplay.Data;
using CarXTowerDefence.Gameplay.Towers;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Bootstrap
{
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField] private TowerDataSO[] m_towerConfigs;
        [SerializeField] private Transform[] m_spawnPoints;

        [SerializeField] private Transform m_container;

        private TowerFactory m_factory;

        public void Spawn()
        {
            if(m_factory is null)
            {
                m_factory = new TowerFactory(m_container);
            }

            for (int i = 0; i < m_spawnPoints.Length; i++)
            {
                var config = GetRandomConfig();
                var tower = m_factory.Create(config, m_spawnPoints[i].position, m_spawnPoints[i].rotation);
            }
        }

        private TowerDataSO GetRandomConfig() =>
            m_towerConfigs[Random.Range(0, m_towerConfigs.Length)];
    }
}