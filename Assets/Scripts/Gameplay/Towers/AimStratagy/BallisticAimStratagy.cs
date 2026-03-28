using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers.AimStrategy
{
    public class BallisticAimStratagy : IAimStrategy
    {
        private const int iterationCount = 32;
        private const float maxFlightTime = 10f;

        public bool TryGetAimPoint(
            Vector3 shooterPosition,
            Vector3 targetPosition,
            Vector3 targetVelocity,
            float projectileSpeed,
            out Vector3 aimPoint)
        {
            aimPoint = Vector3.zero;

            float gravity = Mathf.Abs(Physics.gravity.y);

            float flightTime = Vector3.Distance(shooterPosition, targetPosition) / Mathf.Max(projectileSpeed, 0.01f);
            flightTime = Mathf.Min(flightTime, maxFlightTime);

            Vector3 predictedTarget = targetPosition;

            for (int i = 0; i < iterationCount; i++)
            {
                predictedTarget = targetPosition + targetVelocity * flightTime;

                Vector3 toTarget = predictedTarget - shooterPosition;
                Vector3 horizontalToTarget = new Vector3(toTarget.x, 0f, toTarget.z);

                float horizontalDistance = horizontalToTarget.magnitude;
                float verticalDistance = toTarget.y;

                float speedSquared = projectileSpeed * projectileSpeed;

                float discriminant = speedSquared * speedSquared - gravity * 
                    (gravity * horizontalDistance * horizontalDistance + 2 
                    * verticalDistance * speedSquared);

                if (discriminant < 0f)
                {
                    return false;
                }

                float sqrtDiscriminant = Mathf.Sqrt(discriminant);
                float launchAngle = Mathf.Atan((speedSquared - sqrtDiscriminant) / (gravity * horizontalDistance));
                float cosAngle = Mathf.Cos(launchAngle);

                flightTime = horizontalDistance / (projectileSpeed * cosAngle);
                flightTime = Mathf.Min(flightTime, maxFlightTime);
            }

            Vector3 finalToTarget = predictedTarget - shooterPosition;
            Vector3 finalHorizontal = new Vector3(finalToTarget.x, 0f, finalToTarget.z);

            float finalHorizontalDistance = finalHorizontal.magnitude;
            float finalVerticalDistance = finalToTarget.y;

            float finalSpeedSquared = projectileSpeed * projectileSpeed;

            float finalDiscriminant = finalSpeedSquared * finalSpeedSquared - gravity 
                * (gravity * finalHorizontalDistance * finalHorizontalDistance + 2 * finalVerticalDistance * finalSpeedSquared);

            if (finalDiscriminant < 0f)
            {
                return false;
            }

            float finalSqrt = Mathf.Sqrt(finalDiscriminant);
            float finalLaunchAngle = Mathf.Atan((finalSpeedSquared - finalSqrt) / (gravity * finalHorizontalDistance));

            Vector3 horizontalDirection = finalHorizontal.normalized;

            Vector3 launchDirection =
                horizontalDirection * Mathf.Cos(finalLaunchAngle) +
                Vector3.up * Mathf.Sin(finalLaunchAngle);

            aimPoint = shooterPosition + launchDirection;

            return true;
        }
    }
}