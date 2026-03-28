namespace CarXTowerDefence.Gameplay.Monsters
{
    public interface IHealth 
    {
        public void Increase(float amount);

        public void Decrease(float amount);
    }
}