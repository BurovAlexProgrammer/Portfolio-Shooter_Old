using _Project.Scripts.Main.Services;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
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

        private bool _dialogOpened;

        private void Awake()
        {
            _restartGameButton.onClick.AddListener(RestartGame);
            _returnGameButton.onClick.AddListener(ReturnGame);
            _quitGameButton.onClick.AddListener(ShowQuitGameDialog);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _musicToggle.onValueChanged.AddListener(OnMusicSwitch);
            _soundsToggle.onValueChanged.AddListener(OnSoundsSwitch);
            _quitGameDialog.Confirm += OnQuitDialogConfirm;
            _quitGameDialog.Showed += OnDialogOpened;
            _quitGameDialog.Closed += OnDialogClosed;
            _canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }
        
        private void Start()
        {
            _musicToggle.isOn = _settingsService.Audio.MusicEnabled;
            _soundsToggle.isOn = _settingsService.Audio.SoundEnabled;
        }

        private void OnDestroy()
        {
            _restartGameButton.onClick.RemoveListener(RestartGame);
            _returnGameButton.onClick.RemoveListener(ReturnGame);
            _quitGameButton.onClick.RemoveListener(ShowQuitGameDialog);
            _mainMenuButton.onClick.RemoveListener(GoToMainMenu);
            _musicToggle.onValueChanged.RemoveListener(OnMusicSwitch);
            _soundsToggle.onValueChanged.RemoveListener(OnSoundsSwitch);
            _quitGameDialog.Confirm -= OnQuitDialogConfirm;
            _quitGameDialog.Showed -= OnDialogOpened;
            _quitGameDialog.Closed -= OnDialogClosed;
        }

        private void OnDialogOpened()
        {
            _dialogOpened = true;
        }

        private void OnDialogClosed()
        {
            _dialogOpened = false;
        }

        private async void GoToMainMenu()
        {
            await Close();
            _gameManager.RestoreTimeSpeed();
            _gameManager.GoToMainMenu();
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
            _quitGameDialog.Show().Forget();
        }

        private void OnQuitDialogConfirm(bool result)
        {
            QuitDialogConfirmAsync(result).Forget();
        }
        
        private async UniTaskVoid QuitDialogConfirmAsync(bool result)
        {
            if (result)
            {
                await Close();
                _gameManager.RestoreTimeSpeed();
                _gameManager.QuitGame();
                return;
            }
        
            _quitGameDialog.Close().Forget();
        }
    }
}