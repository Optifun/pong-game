namespace Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string BattleScene = "Battle";
        private readonly GameStateMachine _stateMachine;

        public MainMenuState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _stateMachine.Enter<LoadLevelState, string>(BattleScene);
        }
    }
}