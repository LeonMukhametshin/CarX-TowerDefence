namespace CarXTowerDefence.Gameplay.Towers
{
    public abstract class TowerState : IState
    {
        protected StateMachine fsm;

        protected TowerState(
            StateMachine fsm)
        {
            this.fsm = fsm;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }
    }
}