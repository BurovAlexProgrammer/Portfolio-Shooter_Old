using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class QuitGame : IGameState
        {
            public async UniTask EnterState()
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            public async UniTask ExitState()
            {
            }

            public async UniTask Update()
            {
            }
        }
    }
}