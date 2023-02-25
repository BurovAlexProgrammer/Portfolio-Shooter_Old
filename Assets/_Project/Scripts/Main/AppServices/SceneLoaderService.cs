using System;
using System.Collections.Generic;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices.Base;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class SceneLoaderService : MonoServiceBase
    {
        [SerializeField] private ScenePicker _mainMenuScene;
        [SerializeField] private CanvasGroup _blackFrame;
        [SerializeField] private AnimationCurve _fadeCurve;

        private Scene _currentScene;
        private Scene _preparedScene;
        private Scene _initialScene;
        private readonly Dictionary<Scenes, string> _sceneNames = new()
        {
            { Scenes.Boot, "Boot" },
            { Scenes.MainMenu, "MainMenu" },
            { Scenes.MiniGameLevel, "MiniGameLevel" },
        };

        [Inject]
        protected override void Construct()
        {
            _initialScene = SceneManager.GetActiveScene();
            _blackFrame.alpha = 1f;
            ShowScene();
        }

        public async UniTask LoadSceneAsync(Scenes scene)
        {
            var sceneName = GetSceneName(scene);
            await UniTask.WhenAll(HideScene(), PrepareScene(sceneName));
            ActivatePreparedScene();
            ShowScene();
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
        
        public async void UnloadActiveScene()
        {
            await SceneManager.UnloadSceneAsync(_currentScene);
        }
        
        public void ShowScene()
        {
            _blackFrame.gameObject.SetActive(true);
            _blackFrame
                .DOFade(0f, 0.5f)
                .From(1f)
                .SetEase(_fadeCurve)
                .OnComplete(() => _blackFrame.gameObject.SetActive(false));
        }

        public async UniTask HideScene()
        {
            _blackFrame.gameObject.SetActive(true);
            await _blackFrame
                .DOFade(1f, 0.3f)
                .From(0f)
                .AsyncWaitForCompletion();
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

        private void ActivatePreparedScene()
        {
            _preparedScene.SetActive(true);
            SceneManager.SetActiveScene(_preparedScene);
            SceneManager.UnloadSceneAsync(_currentScene);
        }

        public enum Scenes
        {
            Boot,
            MainMenu,
            Intro,
            MiniGameLevel,
        }

        public bool InitialSceneEquals(Scenes scene)
        {
            if (_initialScene.name == null)
            {
                _initialScene = SceneManager.GetActiveScene();
            }
            
            return GetSceneName(scene).Equals(_initialScene.name);
        }

        private string GetSceneName(Scenes scene)
        {
            if (_sceneNames.ContainsKey(scene)) return _sceneNames[scene];

            throw new Exception("Scene Key not found!");
        }
    }
}