namespace Infrastructure.States
{
    public class ScoreTableState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public ScoreTableState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}