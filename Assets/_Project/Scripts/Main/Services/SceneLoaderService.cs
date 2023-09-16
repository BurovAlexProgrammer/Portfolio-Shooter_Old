using Cysharp.Threading.Tasks;
using Main.DTOs;
using Main.Extension;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Main.Services
{
    public class SceneLoaderService : IService, IConstruct
    {
        private Scene _currentScene;
        private Scene _preparedScene;
        private Scene _initialScene;
        private Scene _bootScene;
        
        public Scene InitialScene => _initialScene;
        public bool IsInitialScene(string sceneName) => _currentScene.name == sceneName;

        public void Construct()
        {
            _currentScene = _initialScene = SceneManager.GetActiveScene();
            _bootScene = SceneManager.GetSceneByName(Scenes.Boot);
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {
            await UniTask.WhenAll(PrepareScene(sceneName));
            SwitchToPreparedScene();
        }

        public async void ReloadActiveScene()
        {
            await SceneManager.UnloadSceneAsync(_currentScene);
            var asyncOperationHandle = Addressables.LoadSceneAsync(_currentScene.name, LoadSceneMode.Additive);
            await asyncOperationHandle.Task;
            var sceneInstance = asyncOperationHandle.Result;
            _preparedScene = sceneInstance.Scene;
            _preparedScene.SetActive(false);
        }

        public void UnloadCurrentScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            var newScene = SceneManager.CreateScene("Empty");
            newScene.SetActive(true);

            SceneManager.UnloadSceneAsync(currentScene);
        }

        public async void UnloadActiveScene()
        {
            await SceneManager.UnloadSceneAsync(_currentScene);
        }

        public bool IsCustomScene()
        {
            return _initialScene != _bootScene;
        }

        private async UniTask PrepareScene(string sceneName)
        {
            _currentScene = SceneManager.GetActiveScene();
            var asyncOperationHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await asyncOperationHandle.Task;
            var sceneInstance = asyncOperationHandle.Result;
            _preparedScene = sceneInstance.Scene;
            _preparedScene.SetActive(false);
        }

        private void SwitchToPreparedScene()
        {
            _preparedScene.SetActive(true);
            SceneManager.SetActiveScene(_preparedScene);
            SceneManager.UnloadSceneAsync(_currentScene);
        }
    }
}