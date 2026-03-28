using CarXTowerDefence.Gameplay.Combat;
using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters
{
    [RequireComponent(typeof(MoveToTarget))]
    [RequireComponent(typeof(MonsterStats))]
    public class Monster : MonoBehaviour, ITargetable
    {
        public event Action<Monster> died;
        public event Action<Monster> reachedTarget;

        [SerializeField] private MonsterStats m_stats;
        [SerializeField] private MoveToTarget m_moveToTarget;

        private DamageReservationHandler m_damageReservation = new();

        public Vector3 position => transform.position;
        public Vector3 velocity => m_moveToTarget.velocity;
        public bool isTargetable => 
            m_stats.health.value > 0 &&
            availableHealth > 0 &&
            m_stats.isInitalize;

        public float availableHealth => 
            m_stats.health.value - m_reservedDamage;

        private float m_reservedDamage;

        public void ReserveDamage(float amount) => 
            m_damageReservation.ReserveDamage(amount);

        public void ReleaseReservedDamage(float amount) => 
            m_damageReservation.ReleaseReservedDamage(amount);

        private void OnValidate()
        {
            if(!m_moveToTarget)
            {
                m_moveToTarget = GetComponent<MoveToTarget>();
            }
            if(!m_stats)
            {
                m_stats = GetComponent<MonsterStats>();
            }
        }

        private void Subscribe()
        {
            m_stats.health.currentValueZero += OnDied;
            m_moveToTarget.reachedTarget += OnMovedToTarget;
        }

        private void Unsubscribe()
        {
            m_stats.health.currentValueZero -= OnDied;
            m_moveToTarget.reachedTarget -= OnMovedToTarget;
        }

        public void Initialize(MonsterStatsData monsterValues, Vector3 targetPosition)
        {
            m_stats.Initialize(monsterValues);
            m_moveToTarget.Initialize(monsterValues.speed, targetPosition);

            Subscribe();
        }

        private void OnMovedToTarget()
        {
            Unsubscribe();
            Reset();
            reachedTarget?.Invoke(this);
        }

        private void Reset()
        {
            Unsubscribe();
            m_damageReservation.Reset();
            m_moveToTarget.Release();
            m_stats.Release();
        }

        private void OnDied()
        {
            Reset();
            died?.Invoke(this);
        }
    }
}