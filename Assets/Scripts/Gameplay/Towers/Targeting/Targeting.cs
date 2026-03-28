using CarXTowerDefence.Gameplay.Combat;
using System.Collections.Generic;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers
{
    public sealed class Targeting : ITargeting
    {
        private const int bufferCapacity = 16;
        private readonly List<ITargetable> m_targets = new();

        private Collider[] m_buffer = new Collider[bufferCapacity];

        private Vector3 m_currentPosition;
        private LayerMask m_targetLayer;
        private float m_detectRadius;

        public Targeting(float detectRadius, LayerMask targetLayer)
        {
            m_detectRadius = detectRadius;
            m_targetLayer = targetLayer;
        }

        public void SetCurrentPosition(Vector3 position) => 
             m_currentPosition = position;

        public void UpdateTargets()
        {
            Detect();
            Validate();
        }

        private void Detect()
        {
            int count = Physics.OverlapSphereNonAlloc(
                m_currentPosition,
                m_detectRadius,
                m_buffer,
                m_targetLayer,
                QueryTriggerInteraction.Collide);

            for (int i = 0; i < count; i++)
            {
                var collider = m_buffer[i];

                if (!collider.TryGetComponent(out ITargetable target) 
                    || !target.isTargetable)
                {
                    continue;
                }

                if (!m_targets.Contains(target))
                {
                    m_targets.Add(target);
                }
            }
        }

        private void Validate()
        {
            for (int i = m_targets.Count - 1; i >= 0; i--)
            {
                var target = m_targets[i];

                if (target == null)
                {
                    m_targets.RemoveAt(i);
                    continue;
                }

                var targetMonobehaviour = target as MonoBehaviour;

                if (!targetMonobehaviour.gameObject.activeInHierarchy)
                {
                    m_targets.RemoveAt(i);
                    continue;
                }

                float distance = (target.position - m_currentPosition).sqrMagnitude;

                if (distance > m_detectRadius * m_detectRadius)
                {
                    m_targets.RemoveAt(i);
                    continue;
                }
            }
        }

        public ITargetable GetBestTarget()
        {
            float bestDistance = float.MaxValue;
            ITargetable bestTarget = null;

            foreach (var target in m_targets)
            {
                float distance = (target.position - m_currentPosition).sqrMagnitude;

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestTarget = target;
                }
            }

            return bestTarget;
        }
    }
}