namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SpawnPointTag = "SpawnPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }


        private void InitGameWorld()
        {
        }
    }
}