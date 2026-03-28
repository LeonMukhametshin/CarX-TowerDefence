using CarXTowerDefence.Gameplay.Monsters;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Effects.Implementations
{
    public class AttackEffect : IEffect, IDamage
    {
        [field: SerializeField][field: Min(0)] public float value { get; private set; } = 10f;

        public void Apply(IEffectable effectable)
        {
            if(effectable is IHealth health)
            {
                health.Decrease(value);
            }
        }
    }
}