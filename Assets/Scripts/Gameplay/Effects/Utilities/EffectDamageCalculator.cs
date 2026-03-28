using System.Collections.Generic;

namespace CarXTowerDefence.Gameplay.Effects.Utilities
{
    public static class EffectDamageCalculator
    {
        public static float CalculateDamage(IReadOnlyList<IEffect> effects)
        {
            float total = 0f;

            foreach (var effect in effects)
            {
                if (effect is IDamage damage)
                {
                    total += damage.value;
                }
            }

            return total;
        }
    }
}