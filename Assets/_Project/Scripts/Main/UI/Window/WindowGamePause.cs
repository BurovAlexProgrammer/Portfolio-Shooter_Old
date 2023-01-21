using _Project.Scripts.Main.Services;
using _Project.Scripts.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Main.UI.Window
{
    public class WindowGamePause : MonoBehaviour
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private Button _returnGameButton;
        [SerializeField] private Button _quitGameButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private DialogView _quitGameDialog;

        [Inject] private GameManagerService _gameManager;
        [Inject] private SettingsService _settingsService;

        private void Awake()
        {
            _returnGameButton.onClick.AddListener(ReturnGame);
            _quitGameButton.onClick.AddListener(ShowQuitGameDialog);
            _musicToggle.onValueChanged.AddListener(OnMusicSwitch);
            _soundsToggle.onValueChanged.AddListener(OnSoundsSwitch);
            _quitGameDialog.OnConfirm += OnQuitDialogResult; 
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
            _returnGameButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
            _musicToggle.onValueChanged.RemoveAllListeners();
            _soundsToggle.onValueChanged.RemoveAllListeners();
        }

        public async void Show()
        {
            gameObject.SetActive(true);
            await transform.DOScale(1f, 0.3f).From(0f)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
        }

        public async void Close()
        {
            _canvasGroup.interactable = false;
            await transform.DOScale(0f, 0.3f)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }

        private void ReturnGame()
        {
            Services.Services.GameManagerService.ReturnGame();
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

        private void OnQuitDialogResult(bool result)
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