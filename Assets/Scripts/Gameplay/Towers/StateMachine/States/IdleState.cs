namespace CarXTowerDefence.Gameplay.Towers
{
    public class IdleState : TowerState
    {
        private ITargeting targeting;

        public IdleState(
            StateMachine fsm, 
            ITargeting targeting) 
            : base(fsm)
        {
            this.targeting = targeting;
        }

        public override void Update()
        {
            base.Update();

            if (targeting.GetBestTarget() is not null)
            {
                fsm.ChangeState<СapturingTargetState>();
            }
        }
    }
}