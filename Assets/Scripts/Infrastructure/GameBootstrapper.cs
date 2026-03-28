using CarXTowerDefence.Gameplay.Monsters;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private MonsterSpawner m_monsterSpawner;
        [SerializeField] private Transform m_monsterTarget;

        [SerializeField] private TowerSpawner m_towerSpawner;

        public void Initialize()
        {
            m_monsterSpawner.Initialize(m_monsterTarget);
            m_towerSpawner.Spawn();
        }
    }
}