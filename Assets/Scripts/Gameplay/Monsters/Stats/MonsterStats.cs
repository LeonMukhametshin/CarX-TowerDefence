using CarXTowerDefence.Gameplay.Effects;
using CarXTowerDefence.Gameplay.Monsters.Config;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters
{
    public class MonsterStats : MonoBehaviour, IEffectable, IHealth
    {
        public Stat health { get; private set; } = new();
        public Stat movementSpeed { get; private set; } = new();

        [SerializeField] private StatView m_healthView;

        public bool isInitalize { get; private set; }

        public void Initialize(MonsterStatsData config)
        {
            if(isInitalize)
            {
                return;
            }

            health.Initialize(config.maxHealth);
            movementSpeed.Initialize(config.speed);

            m_healthView.Construct(health);

            isInitalize = true;
        }

        public void Increase(float amount) => 
            health?.Increase(amount);

        public void Decrease(float amount) => 
            health?.Decrease(amount);

        public void Release()
        {
            isInitalize = false;
        }
    }
}