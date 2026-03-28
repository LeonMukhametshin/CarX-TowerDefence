using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class MoveToTarget : MonoBehaviour
    {
        public event Action reachedTarget;
        public Vector3 velocity { get; private set; }

        [SerializeField] private Rigidbody m_rigidbody;

        private Vector3 m_targetPosition;
        private float m_speed;
        private bool m_isMoving;

        private void OnValidate()
        {
            if (!m_rigidbody)
            {
                m_rigidbody = GetComponent<Rigidbody>();
            }
        }

        public void Initialize(float speed, Vector3 targetPosition)
        {
            m_speed = speed;
            m_targetPosition = targetPosition;
            m_isMoving = true;

            UpdateVelocity();
        }

        private void FixedUpdate()
        {
            if (!m_isMoving)
            {
                return;
            }

            Vector3 newPosition = Vector3.MoveTowards(
                m_rigidbody.position,
                m_targetPosition,
                m_speed * Time.fixedDeltaTime);

            MoveTo(newPosition);

            if (HasReachedTarget(newPosition))
            {
                Stop();
            }
        }

        private void MoveTo(Vector3 target)
        {
            Vector3 previousPosition = m_rigidbody.position;
            m_rigidbody.MovePosition(target);
            velocity = (target - previousPosition) / Time.fixedDeltaTime;
        }

        private bool HasReachedTarget(Vector3 position)
        {
            return (m_targetPosition - position).sqrMagnitude <= Mathf.Epsilon;
        }

        private void UpdateVelocity()
        {
            Vector3 direction = (m_targetPosition - m_rigidbody.position).normalized;
            velocity = direction * m_speed;
        }

        private void Stop()
        {
            m_isMoving = false;
            velocity = Vector3.zero;
            reachedTarget?.Invoke();
        }

        public void Release()
        {
            m_isMoving = false;
            velocity = Vector3.zero;
        }
    }
}