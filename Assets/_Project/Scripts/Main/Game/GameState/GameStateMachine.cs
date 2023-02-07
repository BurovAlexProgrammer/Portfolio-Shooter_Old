using System;
using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using SceneName = _Project.Scripts.Main.Services.SceneLoaderService.Scenes;

namespace _Project.Scripts.Main.Game.GameState
{
    public class GameStateMachine : MonoBehaviour
    {
        public event Action StateChanged;

        private IGameState _activeState;

        [Inject] private SceneLoaderService _sceneLoader;

        public IGameState ActiveState => _activeState;

        public async UniTaskVoid Init()
        {
            if (_sceneLoader.InitialSceneEquals(SceneName.Boot))
            {
                await SetState(new GameStates.Bootstrap());
                await SetState(new GameStates.MainMenu());
                return;
            }

            await SetState(new GameStates.CustomScene());
        }

        public async UniTask SetState(IGameState newState)
        {
            if (_activeState == newState)
            {
                Debug.Log("GameState Enter: " + newState + " (Already entered, skipped)", this);
                return;
            }

            if (_activeState != null)
            {
                Debug.Log("GameState Exit: " + _activeState, this);
                await _activeState.ExitState();
            }

            _activeState = newState;

            Debug.Log("GameState Enter: " + newState, this);
            await _activeState.EnterState();
            StateChanged?.Invoke();
        }
    }
}