using UnityEngine;
using UnityEngine.UI;

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
        

        void Start()
        {
            _settingsButton.onClick.AddListener(() => _menuController.SetState(MenuStates.Settings));
            _backFromSettings.onClick.AddListener(() => _menuController.SetState(MenuStates.MainMenu));
            _buttonQuit.onClick.AddListener(() => _menuController.SetState(MenuStates.QuitGame));
            _buttonQuitNo.onClick.AddListener(() => _menuController.SetState(MenuStates.MainMenu));
            _buttonQuitYes.onClick.AddListener(_menuController.QuitGame);
        }
    }
}
