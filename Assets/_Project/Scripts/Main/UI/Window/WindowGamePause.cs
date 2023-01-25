using _Project.Scripts.Main.Services;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Main.UI.Window
{
    public class WindowGamePause : WindowView
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private Button _returnGameButton;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _quitGameButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private DialogView _quitGameDialog;

        [Inject] private GameManagerService _gameManager;
        [Inject] private SettingsService _settingsService;

        private void Awake()
        {
            _restartGameButton.onClick.AddListener(RestartGame);
            _returnGameButton.onClick.AddListener(ReturnGame);
            _quitGameButton.onClick.AddListener(ShowQuitGameDialog);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _musicToggle.onValueChanged.AddListener(OnMusicSwitch);
            _soundsToggle.onValueChanged.AddListener(OnSoundsSwitch);
            _quitGameDialog.Confirm += OnQuitDialogConfirm; 
            _canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }

        private void GoToMainMenu()
        {
            _gameManager.GoToMainMenu();
        }

        private void Start()
        {
            _musicToggle.isOn = _settingsService.Audio.MusicEnabled;
            _soundsToggle.isOn = _settingsService.Audio.SoundEnabled;
        }

        private void OnDestroy()
        {
            _restartGameButton.onClick.RemoveAllListeners();
            _returnGameButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _musicToggle.onValueChanged.RemoveAllListeners();
            _soundsToggle.onValueChanged.RemoveAllListeners();
        }

        private async void RestartGame()
        {
            await Close();
            _gameManager.RestartGame();
        }
        
        private async void ReturnGame()
        {
            await Close();
            _gameManager.ReturnGame();
        }

        private void OnMusicSwitch(bool newValue)
        {
            _settingsService.Audio.MusicEnabled = newValue;
            _settingsService.Save();
        }
    
        private void OnSoundsSwitch(bool newValue)
        {
            _settingsService.Audio.SoundEnabled = newValue;
            _settingsService.Save();
        }
    
        private void ShowQuitGameDialog()
        {
            _ = _quitGameDialog.Show();
        }

        private void OnQuitDialogConfirm(bool result)
        {
            if (result)
            {
                _gameManager.QuitGame();
                return;
            }
        
            _ = _quitGameDialog.Close();
        }
    }
}