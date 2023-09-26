using Main.Game.GameState;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;

namespace Main.Menu
{
    public class MainMenuController : MenuController
    {
        private GameManagerService _gameManager;

        protected override void Init()
        {
            base.Init();
            _gameManager = Context.Resolve<GameManagerService>();
        }

        private void Start()
        {
            EnterState(MenuStates.MainMenu).Forget();
        }

        public void QuitGame()
        {
            _gameManager.QuitGame();
        }

        public void StartNewGame()
        {
            _gameManager.SetGameState<GameStates.PlayNewGame>().Forget();
        }

        protected override async UniTask EnterState(MenuStates newState)
        {
            await base.EnterState(newState);
        }

        protected override async UniTask ExitState(MenuStates oldState)
        {
            await base.ExitState(oldState);
        }
    }
}