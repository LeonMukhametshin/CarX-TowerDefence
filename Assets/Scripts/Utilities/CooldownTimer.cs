using System;
using UnityEngine;

namespace CarXTowerDefence.Utilities
{
    public sealed class CooldownTimer
    {
        public event Action timerDone;

        private float m_startTime;
        private float m_duration;
        private float m_targetTime;

        private bool isActive;

        public CooldownTimer(float duration)
        {
            this.m_duration = duration;
        }

        public void StartTimer()
        {
            m_startTime = Time.time;
            m_targetTime = m_startTime + m_duration;
            isActive = true;
        }

        public void StopTimer() => 
            isActive = false;

        public void Tick()
        {
            if (!isActive)
            {
                return;
            }

            if (Time.time >= m_targetTime)
            {
                StopTimer();
                timerDone?.Invoke();
            }
        }
    }
}