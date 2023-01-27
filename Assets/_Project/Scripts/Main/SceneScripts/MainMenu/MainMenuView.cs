using UnityEngine;
using UnityEngine.UI;
using static _Project.Scripts.Main.Menu.MenuController;

namespace _Project.Scripts.Main.SceneScripts.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private MainMenuController _menuController;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backFromSettings;
        [SerializeField] private Button _buttonQuit;
        [SerializeField] private Button _buttonQuitNo;
        [SerializeField] private Button _buttonQuitYes;
        [SerializeField] private Button _buttonNewGame;
        [SerializeField] private Button _buttonStatistic;
        [SerializeField] private Button _buttonAbout;

        void Start()
        {
            _settingsButton.onClick.AddListener(() => _menuController.SetState(MenuStates.Settings));
            _backFromSettings.onClick.AddListener(() => _menuController.SetState(MenuStates.MainMenu));
            _buttonQuit.onClick.AddListener(() => _menuController.SetState(MenuStates.QuitGame));
            _buttonQuitNo.onClick.AddListener(() => _menuController.SetState(MenuStates.MainMenu));
            _buttonQuitYes.onClick.AddListener(_menuController.QuitGame);
            _buttonNewGame.onClick.AddListener(_menuController.StartNewGame);
            _buttonStatistic.onClick.AddListener(() => _menuController.SetState(MenuStates.Statistic));
            _buttonAbout.onClick.AddListener(() => _menuController.SetState(MenuStates.About));
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveAllListeners();
            _backFromSettings.onClick.RemoveAllListeners();
            _buttonQuit.onClick.RemoveAllListeners();
            _buttonQuitNo.onClick.RemoveAllListeners();
            _buttonQuitYes.onClick.RemoveAllListeners();
            _buttonNewGame.onClick.RemoveAllListeners();
            _buttonStatistic.onClick.RemoveAllListeners();
            _buttonAbout.onClick.RemoveAllListeners();
        }
    }
}