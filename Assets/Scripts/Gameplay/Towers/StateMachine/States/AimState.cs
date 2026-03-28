using CarXTowerDefence.Gameplay.Combat;
using CarXTowerDefence.Gameplay.Towers.Aiming;

namespace CarXTowerDefence.Gameplay.Towers
{
    public class AimState : TowerState
    {
        private IAimer aimer;
        private ITargeting targeting;
        private ITargetable m_target;

        public AimState(
            StateMachine fsm,
            IAimer aimer,
            ITargeting targeting)
            : base(fsm)
        {
            this.aimer = aimer;
            this.targeting = targeting;
        }

        public override void Enter()
        {
            base.Enter();
            m_target = targeting.GetBestTarget();
        }

        public override void Update()
        {
            if(m_target is null || !m_target.isTargetable)
            {
                fsm.ChangeState<IdleState>();
                return;
            }
        }
    }
}