using UnityEngine;

namespace CarXTowerDefence.Gameplay.Projectiles.Implementations
{
    public class LinearProjectile : Projectile
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Vector3 nextPosition = rigidbody.position + m_velocity * Time.fixedDeltaTime;
            rigidbody.MovePosition(nextPosition);
        }
    }
}