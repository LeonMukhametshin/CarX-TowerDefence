namespace CarXTowerDefence.Gameplay.Towers
{
    public struct TowerSettings 
    {
        public float shootInterval { get; private set; }
        public float attackRange { get; private set; }
        public float projectileSpeed { get; private set; }

        public TowerSettings(float shootInterval, float attackRange, float projectileSpeed)
        {
            this.shootInterval = shootInterval;
            this.attackRange = attackRange;
            this.projectileSpeed = projectileSpeed;
        }
    }
}