using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game.GameState;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;

namespace _Project.Scripts.Main.Menu
{
    public class MainMenuController : MenuController
    {
        private SceneLoaderService _sceneLoader;
        private GameManagerService _gameManager;

        protected override void Init()
        {
            base.Init();
            _sceneLoader = Context.GetService<SceneLoaderService>();
            _gameManager = Context.GetService<GameManagerService>();
        }

        private void Start()
        {
            EnterState(MenuStates.MainMenu).Forget();
        }

        public async void QuitGame()
        {
            await _sceneLoader.HideScene();
            _gameManager.QuitGame();
        }

        public void StartNewGame()
        {
            _gameManager.SetGameState<GameStates.PlayNewGame>().Forget();
        }

        protected override async UniTask EnterState(MenuStates newState)
        {
            await base.EnterState(newState);

            switch (newState)
            {
                case MenuStates.MainMenu:
                    break;
                case MenuStates.Settings:
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }

        protected override async UniTask ExitState(MenuStates oldState)
        {
            await base.ExitState(oldState);

            switch (oldState)
            {
                case MenuStates.Settings:
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }
    }
}