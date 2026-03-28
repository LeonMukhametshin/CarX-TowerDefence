using System;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters
{
    public class Stat
    {
        public event Action currentValueZero;
        public event Action<float> valueChanged;

        public float maxValue { get; private set; }

        public float value
        {
            get => m_value;
            set
            {
                float clampedValue = Mathf.Clamp(value, 0f, maxValue);
                
                if (Mathf.Approximately(m_value, value))
                {
                    return;
                }

                m_value = clampedValue;
                valueChanged?.Invoke(m_value);

                if (this.value <= 0f)
                { 
                    currentValueZero?.Invoke();
                }
            }
        }

        private float m_value;

        public void Initialize(float maxValue)
        {
            this.maxValue = maxValue;
            m_value = this.maxValue;
        }

        public void Increase(float amount)
        {
            if(amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Increase amount cannot be hegative");
            }

            value += amount;
        }

        public void Decrease(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Decrease amount cannot be hegative");
            }

            value -= amount;
        }
    }
}