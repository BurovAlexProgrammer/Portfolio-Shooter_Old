using _Project.Scripts.Main.Menu;
using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using Zenject;
using static _Project.Scripts.Extension.Common;

namespace _Project.Scripts.Main.SceneScripts.MainMenu
{
    public class MainMenuController : MenuController
    {
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private GameManagerService _gameManager;
        
        private void Start()
        {
            _ = EnterState(MenuStates.MainMenu);
        }


        public async void QuitGame()
        {
            await _sceneLoader.HideScene();
            _gameManager.QuitGame();
        }

        public void StartNewGame()
        {
            _gameManager.SetGameState(GameStates.PlayGame);
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