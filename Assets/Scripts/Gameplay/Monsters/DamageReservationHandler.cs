using System;

namespace CarXTowerDefence.Gameplay.Combat
{
    public class DamageReservationHandler : IDamageReservable
    {
        private float m_reservedDamage;

        public float reservedDamage => m_reservedDamage;
        public event Action<float> reservedDamageChanged;

        public void ReserveDamage(float amount)
        {
            m_reservedDamage += amount;
            reservedDamageChanged?.Invoke(m_reservedDamage);
        }

        public void ReleaseReservedDamage(float amount)
        {
            m_reservedDamage -= amount;
            if (m_reservedDamage < 0)
            {
                m_reservedDamage = 0;
            }
            reservedDamageChanged?.Invoke(m_reservedDamage);
        }

        public void Reset()
        {
            m_reservedDamage = 0f;
        }
    }
}