using CarXTowerDefence.Gameplay.Combat;
using CarXTowerDefence.Gameplay.Effects;
using CarXTowerDefence.Gameplay.Effects.Utilities;
using CarXTowerDefence.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody rigidbody { get; private set; }
        [SerializeField][Min(0f)] private float m_maxLifetime = 10f;

        protected GameObjectPool m_pool;
        protected CooldownTimer m_lifetimeTimer;
        protected ITargetable m_target;

        protected Vector3 m_velocity;
        protected IReadOnlyList<IEffect> m_effects;

        private bool m_initialized;
        private float m_reservedDamage;

        private void OnValidate()
        {
            if (!rigidbody)
            {
                rigidbody = GetComponent<Rigidbody>();
            }
        }

        private void Awake()
        {
            m_lifetimeTimer = new CooldownTimer(m_maxLifetime);
        } 

        public virtual void Initialize(
            Vector3 startPosition,
            Vector3 velocity,
            ITargetable target,
            IReadOnlyList<IEffect> effects)
        {
            m_target = target;
            m_velocity = velocity;
            m_effects = effects;

            transform.position = startPosition;

            SetupTimer();
            ReserveDamage();
            ApplyVelocity();

            m_initialized = true;
        }

        protected virtual void FixedUpdate() { }

        private void Update() => m_lifetimeTimer?.Tick();

        protected virtual void ApplyVelocity() { }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEffectable effectable))
            {
                m_effects.ApplyEffect(effectable);
            }
            Release();
        }

        public void SetPool(GameObjectPool pool) => 
            m_pool = pool;

        private void SetupTimer()
        {
            if (m_lifetimeTimer is not null) m_lifetimeTimer.timerDone -= Release;
            m_lifetimeTimer = new CooldownTimer(m_maxLifetime);
            m_lifetimeTimer.timerDone += Release;
            m_lifetimeTimer.StartTimer();
        }

        private void ReserveDamage()
        {
            float damage = EffectDamageCalculator.CalculateDamage(m_effects);
            if (damage <= 0f || m_target is null)
            {
                return;
            }

            m_reservedDamage = damage;
            m_target.ReserveDamage(damage);
        }

        private void ReleaseReservedDamage()
        {
            if (m_target is not null && m_reservedDamage > 0)
            {
                m_target.ReleaseReservedDamage(m_reservedDamage);
                m_reservedDamage = 0f;
            }
        }

        protected virtual void Release()
        {
            ReleaseReservedDamage();
            ResetState();
            ReturnToPool();
        }

        private void ResetState()
        {
            if (m_lifetimeTimer is not null)
            {
                m_lifetimeTimer.timerDone -= Release;
                m_lifetimeTimer = null;
            }
            m_target = null;
            m_initialized = false;
        }

        private void ReturnToPool()
        {
            if (m_pool is not null)
            {
                m_pool.Return(gameObject);
            }

            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}