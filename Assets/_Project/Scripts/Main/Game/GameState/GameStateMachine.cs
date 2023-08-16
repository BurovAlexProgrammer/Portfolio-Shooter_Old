#nullable enable
using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Main.Contexts;
using Main.Services;
using UnityEngine;
using SceneName = Main.Services.SceneLoaderService.Scenes;

namespace Main.Game.GameState
{
    [UsedImplicitly]
    public class GameStateMachine
    {
        public event Action StateChanged;

        private IGameState _activeState;
        private SceneLoaderService _sceneLoader;
        
        public IGameState ActiveState => _activeState;

        public GameStateMachine()
        {
            _sceneLoader = Context.GetService<SceneLoaderService>();
        }
        
        public async UniTask Start()
        {
            if (_sceneLoader.InitialSceneEquals(SceneName.Boot))
            {
                await SetState<GameStates.Bootstrap>();
                await SetState<GameStates.MainMenu>();
                return;
            }
            
            await SetState<GameStates.CustomScene>();
        }

        public async UniTask SetState<T>() where T : IGameState
        {
            var newStateType = typeof(T);
            
            if (_activeState?.GetType() == newStateType)
            {
                Debug.Log("GameState Enter: " + newStateType.Name + " (Already entered, skipped)");
                return;
            }

            if (_activeState != null)
            {
                Debug.Log("GameState Exit: " + _activeState.GetType().Name);
                await _activeState.ExitState();
            }

            _activeState = Activator.CreateInstance<T>();

            Debug.Log("GameState Enter: " + _activeState.GetType().Name);
            await _activeState.EnterState();
            StateChanged?.Invoke();
        }
    }
}