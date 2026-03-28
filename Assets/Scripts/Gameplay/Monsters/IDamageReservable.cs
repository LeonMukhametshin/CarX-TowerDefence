namespace CarXTowerDefence.Gameplay.Combat
{
    public interface IDamageReservable
    {
        void ReserveDamage(float amount);
        void ReleaseReservedDamage(float amount);
    }
}