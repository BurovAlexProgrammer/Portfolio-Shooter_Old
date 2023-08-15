using System;
using System.Collections.Generic;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Menu
{
    public abstract class MenuController : MonoBehaviour
    {
        [LabeledArray(typeof(MenuStates))]
        [SerializeField] 
        private MenuView[] _menus;
        
        private MenuStates _activeState;
        private MenuStates _prevState;

        protected virtual void Init() {}
        protected virtual void Dispose() {}
        
        private void Awake()
        {
            if (_menus.Length != Enum.GetNames(typeof(MenuStates)).Length)
            {
                throw new Exception("LabeledArray of MenuStates: range error.");
            }

            for (var i = 0; i < _menus.Length; i++)
            {
                _menus[i].GoBack += GoToPrevMenu;
                _menus[i].HideImmediate();
            }

            Init();
        }

        private void OnDestroy()
        {
            for (var i = 0; i < _menus.Length; i++)
            {
                _menus[i].GoBack -= GoToPrevMenu;
            }

            Dispose();
        }

        public async void SetState(MenuStates newState)
        {
            await ExitState(_activeState);
            _prevState = _activeState;
            _activeState = newState;
            await EnterState(newState);
        }

        protected virtual async UniTask EnterState(MenuStates newState)
        {
            Debug.Log("MenuState Enter: " + newState, this);
            var menu = GetMenu(newState);
            await menu.Show();
            menu.Enable();
        }

        protected virtual async UniTask ExitState(MenuStates oldState)
        {
            Debug.Log("MenuState ExitState: " + oldState, this);
            DisableAllMenus();
            await HideAllMenus();
        }
        
        private async UniTask HideAllMenus()
        {
            var tasks = new List<UniTask>();

            for (var i = 0; i < _menus.Length; i++)
            {
                tasks.Add(_menus[i].Hide());
            }

            await UniTask.WhenAll(tasks);
        }
        
        private void DisableAllMenus()
        {
            for (var i = 0; i < _menus.Length; i++)
            {
                _menus[i].Disable();
            }
        }
        
        public void GoToPrevMenu()
        {
            SetState(_prevState);
        }
        
        private MenuView GetMenu(MenuStates states)
        {
            return _menus[(int)states];
        }
        
        public enum MenuStates
        {
            MainMenu,
            Settings,
            QuitGame,
            NewGame,
            Statistic,
            About,
        }
    }
}