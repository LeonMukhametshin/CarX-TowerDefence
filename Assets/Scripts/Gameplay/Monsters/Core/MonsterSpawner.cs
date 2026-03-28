using CarXTowerDefence.Utilities;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters
{
    public sealed class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private MonsterConfig m_monsterConfig;

        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private Transform m_poolRoot;

        [SerializeField] private float m_spawnInterval;
        [SerializeField][Min(0)] private int m_initialPoolSize = 2;

        private GameObjectPool m_monsterPool;
        private CooldownTimer m_spawnTimer;
        private Vector3 m_targetPosition;

        private bool m_isInitialized;

        public void Initialize(Transform targetTransform)
        {
            if (m_isInitialized)
            {
                return;
            }

            m_targetPosition = targetTransform.position;
            m_monsterPool = new GameObjectPool(m_monsterConfig.monsterPrefab, m_initialPoolSize);
            m_spawnTimer = new CooldownTimer(m_spawnInterval);
            m_spawnTimer.timerDone += SpawnMonster;
            m_spawnTimer.StartTimer();

            m_isInitialized = true;
        }

        private void OnDestroy()
        {
            if (m_spawnTimer != null)
            {
                m_spawnTimer.timerDone -= SpawnMonster;
            }
        }

        private void Update()
        {
            if (!m_isInitialized)
            {
                return;
            }

            m_spawnTimer?.Tick();
        }

        private void SpawnMonster()
        {
            GameObject monsterInstance = m_monsterPool.Get();
            monsterInstance.transform.SetParent(m_poolRoot, false);
            monsterInstance.transform.position = m_spawnPoint.position;

            if (!monsterInstance.TryGetComponent(out Monster monster))
            {
                m_monsterPool.Return(monsterInstance);
                m_spawnTimer.StartTimer();
                return;
            }

            Rigidbody rigidbody = monsterInstance.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.position = m_spawnPoint.position;
            }

            UnsubscribeFromMonsterEvents(monster);
            monster.Initialize(m_monsterConfig.baseStats, m_targetPosition);
            SubscribeToMonsterEvents(monster);

            m_spawnTimer.StartTimer();
        }

        private void SubscribeToMonsterEvents(Monster monster)
        {
            monster.reachedTarget += HandleMonsterRelease;
            monster.died += HandleMonsterRelease;
        }

        private void UnsubscribeFromMonsterEvents(Monster monster)
        {
            monster.reachedTarget -= HandleMonsterRelease;
            monster.died -= HandleMonsterRelease;
        }

        private void HandleMonsterRelease(Monster monster)
        {
            if (monster == null)
            {
                return;
            }

            UnsubscribeFromMonsterEvents(monster);
            m_monsterPool.Return(monster.gameObject);
        }
    }
}