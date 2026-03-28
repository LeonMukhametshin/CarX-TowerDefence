using CarXTowerDefence.Gameplay.Combat;
using CarXTowerDefence.Gameplay.Towers.Aiming;
using CarXTowerDefence.Gameplay.Towers.AimStrategy;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers
{
    public class СapturingTargetState : TowerState
    {
        private IAimer aimer;
        private ITargeting targeting;
        private IAimStrategy m_aimStrategy;

        private Transform m_barrel;

        private float m_projectileSpeed;

        private ITargetable m_target;

        public СapturingTargetState(
            StateMachine fsm,
            Transform ballet,
            ITargeting targeting,
            IAimer aimer,
            IAimStrategy aimStrategy,
            float projectileSpeed) 
            : base(fsm)
        {
            this.aimer = aimer;
            this.targeting = targeting;
            this.m_aimStrategy = aimStrategy;
            m_projectileSpeed = projectileSpeed;
            m_barrel = ballet;
        }

        public override void Update()
        {
            var newTarget = targeting.GetBestTarget();

            if (newTarget == null || !newTarget.isTargetable)
            {
                fsm.ChangeState<IdleState>();
                return;
            }

            m_target = newTarget;
            Vector3 aimPoint;

            if (!m_aimStrategy.TryGetAimPoint(
                m_barrel.position, 
                m_target.position, 
                m_target.velocity, 
                m_projectileSpeed,
                out aimPoint))
            {
                Debug.LogWarning($"[{nameof(СapturingTargetState)}] Cannot calculate aim point for target {m_target}");
                return;
            }

            aimer.RotateTowards(aimPoint, () => fsm.ChangeState<AttackState>());
        }
    }
}