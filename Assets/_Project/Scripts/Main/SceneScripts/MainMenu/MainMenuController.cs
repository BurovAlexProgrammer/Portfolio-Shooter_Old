using System;
using System.Collections.Generic;
using _Project.Scripts.Extension.LabeledArray;
using _Project.Scripts.Main.Menu;
using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using static _Project.Scripts.Extension.Common;

namespace _Project.Scripts.Main.SceneScripts.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [LabeledArray(typeof(MenuStates))]
        [SerializeField]
        private MenuView[] _menus;
        
        private MenuStates _activeState;
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private GameManagerService _gameManager;

        public MenuStates ActiveState => _activeState;

        private void Awake()
        {
            if (_menus.Length != Enum.GetNames(typeof(MenuStates)).Length)
            {
                throw new Exception("LabeledArray of MenuStates: range error.");
            } 
        }

        private void Start()
        {
            _ = EnterState(MenuStates.MainMenu);
        }

        public async void SetState(MenuStates newState)
        {
            await ExitState(_activeState);
            await EnterState(newState);
        }

        public async void QuitGame()
        {
            await _sceneLoader.HideScene();
            _gameManager.QuitGame();
        }

        private async UniTask EnterState(MenuStates newState)
        {
            Debug.Log("MenuState Enter: " + newState, this);
            var menu = GetMenu(newState);
            await menu.Show();
            menu.Enable();
            
            switch (newState)
            {
                case MenuStates.MainMenu:
                    break;
                case MenuStates.Settings:
                    await EnterStateBoot();
                    break;
                case MenuStates.QuitGame:
                    break;
                case MenuStates.NewGame:
                    break;
            }
        }

        private async UniTask ExitState(MenuStates oldState)
        {
            Debug.Log("GameState ExitState: " + oldState, this);
            DisableAllMenus();
            await HideAllMenus();
                
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

        private MenuView GetMenu(MenuStates states)
        {
            return _menus[(int)states];
        }

        private void DisableAllMenus()
        {
            foreach (var menu in _menus)
            {
                menu.Disable();
            }
        }

        private async UniTask HideAllMenus()
        {

            var tasks = new List<UniTask>();
            foreach (var menu in _menus)
            {
                tasks.Add(menu.Hide());
            }
            
            await UniTask.WhenAll(tasks);
        }

        private async UniTask EnterStateBoot()
        {
            await Wait(1f);
        }

        private void ShowMenu()
        {
            
        }
    }

    public enum MenuStates
    {
        MainMenu,
        Settings,
        QuitGame,
        NewGame,
    }
}