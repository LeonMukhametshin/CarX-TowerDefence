using CarXTowerDefence.Gameplay.Towers.Aiming;
using CarXTowerDefence.Gameplay.Towers.AimStrategy;
using CarXTowerDefence.Gameplay.Towers.Shooting;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Towers
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private Transform m_shootPoint;
        [SerializeField] private Transform m_barrelTransform;

        private StateMachine fsm;

        private ITargeting m_targeting;
        private IAimer m_aimer;
        private IShooter m_shooter;
        private IAimStrategy m_aimStrategy;

        private bool m_initilized;

        //TODO remove
        private float m_detectRadius;

        public void Initialize(
            TowerSettings config, 
            ITargeting targeting, 
            IAimer aimer, 
            IShooter shooter,
            IAimStrategy aimStrategy)
        {
            m_targeting = targeting;
            m_aimer = aimer;
            m_shooter = shooter;
            m_aimStrategy = aimStrategy;

            m_shooter.SetShootPoint(m_shootPoint);
            m_aimer.SetBarrel(m_barrelTransform);
            m_targeting.SetCurrentPosition(transform.position);

            fsm = new StateMachine();

            fsm.Initialize(
                new IdleState(fsm, m_targeting),
                new СapturingTargetState(fsm, m_shootPoint, m_targeting, m_aimer, m_aimStrategy, config.projectileSpeed),
                new AimState(fsm, m_aimer, m_targeting),
                new AttackState(fsm, m_shooter, m_targeting),
                new CooldownState(fsm, config.shootInterval));

            fsm.ChangeState<IdleState>();

            m_initilized = true;

            m_detectRadius = config.attackRange;
        }

        private void Update()
        {
            if(!m_initilized)
            {
                return;
            }

            fsm.UpdateState();
            m_targeting.UpdateTargets();
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 0.9f, 0.74f, 0.2f);
            Gizmos.DrawSphere(transform.position, m_detectRadius);
        }
    }
}