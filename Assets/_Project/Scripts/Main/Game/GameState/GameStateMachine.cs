using System;
using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using SceneName = _Project.Scripts.Main.AppServices.SceneLoaderService.Scenes;

namespace _Project.Scripts.Main.Game.GameState
{
    [UsedImplicitly]
    public class GameStateMachine
    {
        [Inject] private DiContainer _diContainer;
        
        public event Action StateChanged;

        private IGameState _activeState;
        private SceneLoaderService _sceneLoader;
        
        public IGameState ActiveState => _activeState;

        [Inject]
        public void Construct(SceneLoaderService sceneLoaderService)
        {
            _sceneLoader = sceneLoaderService;
            Init();
        }
        
        public async UniTask Init()
        {
            if (_sceneLoader.InitialSceneEquals(SceneName.Boot))
            {
                Debug.LogWarning("Init");
                await SetState<GameStates.Bootstrap>();
                Debug.LogWarning("After boot");
                await SetState<GameStates.MainMenu>();
                Debug.LogWarning("After MainMenu");
                return;
            }
            
            await SetState<GameStates.CustomScene>();
        }

        public async UniTask SetState<T>() where T : IGameState
        {
            var newStateType = typeof(T);
            
            if (_activeState?.GetType() == newStateType)
            {
                Debug.Log("GameState Enter: " + newStateType + " (Already entered, skipped)");
                return;
            }

            if (_activeState != null)
            {
                Debug.Log("GameState Exit: " + _activeState);
                await _activeState.ExitState();
            }

            _activeState = _diContainer.Instantiate<T>();

            Debug.Log("GameState Enter: " + _activeState);
            await _activeState.EnterState();
            StateChanged?.Invoke();
        }
    }
}