using CarXTowerDefence.Gameplay.Towers.Shooting;

namespace CarXTowerDefence.Gameplay.Towers
{
    public class AttackState : TowerState
    {
        private IShooter shooter;
        private ITargeting targeting;

        public AttackState(
            StateMachine fsm,
            IShooter shooter,
            ITargeting targeting)
            : base(fsm)
        {
            this.shooter = shooter;
            this.targeting = targeting;
        }

        public override void Enter()
        {
            base.Enter();

            var target = targeting.GetBestTarget();

            if (target == null || !target.isTargetable)
            {
                fsm.ChangeState<IdleState>();
                return;
            }

            shooter.TryShoot(target);
            fsm.ChangeState<CooldownState>();
        }
    }
}