using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Main.DTOs;
using Main.Extension;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Main.Services
{
    public class SceneLoaderService : IService, IConstruct
    {
        private Dictionary<DTO.Scenes, string> _sceneNames;
        private Scene _currentScene;
        private Scene _preparedScene;
        private Scene _initialScene;
        private Scene _bootScene;
        public Scene InitialScene => _initialScene;

        public void Construct()
        {
            _sceneNames = Enum.GetValues(typeof(DTO.Scenes))
                .Cast<DTO.Scenes>()
                .ToDictionary(x => x, x => x.ToString());
            _currentScene = _initialScene = SceneManager.GetActiveScene();
            _bootScene = SceneManager.GetSceneByName(GetSceneName(DTO.Scenes.Boot));
        }

        public bool InitialSceneEquals(DTO.Scenes scene)
        {
            return _currentScene.name == scene.ToString();
        }

        public async UniTask LoadSceneAsync(DTO.Scenes scene)
        {
            await UniTask.WhenAll(PrepareScene(scene));
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

        private async UniTask PrepareScene(DTO.Scenes scene)
        {
            _currentScene = SceneManager.GetActiveScene();
            var asyncOperationHandle = Addressables.LoadSceneAsync(GetSceneName(scene), LoadSceneMode.Additive);
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

        private string GetSceneName(DTO.Scenes scene)
        {
            if (_sceneNames.TryGetValue(scene, out var result))
            {
                return result;
            }

            throw new Exception("Scene Key not found!");
        }
    }
}