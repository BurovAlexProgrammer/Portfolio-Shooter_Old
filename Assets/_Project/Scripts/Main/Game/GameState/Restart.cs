using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class RestartGame : IGameState
        {
            public async UniTask EnterState()
            {
                var currentScene = SceneManager.GetActiveScene();
                var newScene = SceneManager.CreateScene("Empty");
                newScene.SetActive(true);
            
                await SceneManager.UnloadSceneAsync(currentScene);
            }
        }
    }
}