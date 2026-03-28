namespace CarXTowerDefence.Gameplay.Projectiles.Implementations
{
    public class ParabolaProjectie : Projectile
    {
        protected override void ApplyVelocity()
        {
            rigidbody.linearVelocity = m_velocity;
        }
    }
}