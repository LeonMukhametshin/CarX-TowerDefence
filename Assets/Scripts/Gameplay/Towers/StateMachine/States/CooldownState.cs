using CarXTowerDefence.Utilities;

namespace CarXTowerDefence.Gameplay.Towers
{
    public class CooldownState : TowerState
    {
        private CooldownTimer m_timer;

        public CooldownState(
            StateMachine fsm, 
            float duration) 
            : base(fsm)
        {
            m_timer = new CooldownTimer(duration);
        }

        public override void Enter()
        {
            m_timer.StartTimer();
            m_timer.timerDone += OnTimerDone;
        }

        public override void Exit() => 
            m_timer.timerDone -= OnTimerDone;

        public override void Update() => 
            m_timer.Tick();

        private void OnTimerDone()
        {
            fsm.ChangeState<IdleState>();
        }
    }
}