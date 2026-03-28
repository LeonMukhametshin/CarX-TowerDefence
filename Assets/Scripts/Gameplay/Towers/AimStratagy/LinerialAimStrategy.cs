using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.AimStrategy
{
    public class LinerialAimStrategy : IAimStrategy
    {
        private const int iterationCount = 32;
        private const float maxInterceptTime = 60f;
        private const float minProjectileSpeed = 0.01f;
        private const float convergenceThreshold = 0.001f;

        public bool TryGetAimPoint(
            Vector3 shooterPosition,
            Vector3 targetPosition,
            Vector3 targetVelocity,
            float projectileSpeed,
            out Vector3 aimPoint)
        {
            aimPoint = targetPosition;

            if (projectileSpeed <= minProjectileSpeed)
            {
                return false;
            }

            Vector3 toTarget = targetPosition - shooterPosition;
            float distanceToTarget = toTarget.magnitude;

            if (distanceToTarget < Mathf.Epsilon)
            {
                aimPoint = targetPosition;
                return true;
            }

            float interceptTime = distanceToTarget / projectileSpeed;

            for (int i = 0; i < iterationCount; i++)
            {
                Vector3 predictedPosition = targetPosition + targetVelocity * interceptTime;
                Vector3 toPredicted = predictedPosition - shooterPosition;
                float distanceToPredicted = toPredicted.magnitude;

                float newInterceptTime = distanceToPredicted / projectileSpeed;

                if (Mathf.Abs(newInterceptTime - interceptTime) < convergenceThreshold)
                {
                    interceptTime = newInterceptTime;
                    break;
                }

                interceptTime = newInterceptTime;

                if (interceptTime > maxInterceptTime)
                {
                    interceptTime = maxInterceptTime;
                    break;
                }
            }

            aimPoint = targetPosition + targetVelocity * interceptTime;
            return true;
        }
    }
}