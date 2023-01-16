using System;
using _Project.Scripts.Main.Services;
using _Project.Scripts.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class WindowGamePause : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundsToggle;
    [SerializeField] private Button _returnGameButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private DialogView _quitGameDialog;

    [Inject] private SettingsService _settingsService;
    [Inject] private ControlService _controlService;
    
    private void Awake()
    {
        Services.GameManagerService.SwitchPause += OnSwitchGamePause;
        _returnGameButton.onClick.AddListener(ReturnGame);
        _quitGameButton.onClick.AddListener(ShowQuitGameDialog);
        _musicToggle.onValueChanged.AddListener(OnMusicSwitch);
        _soundsToggle.onValueChanged.AddListener(OnSoundsSwitch);
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
        Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
        _returnGameButton.onClick.RemoveAllListeners();
        _quitGameButton.onClick.RemoveAllListeners();
        _musicToggle.onValueChanged.RemoveAllListeners();
        _soundsToggle.onValueChanged.RemoveAllListeners();
    }

    private void ReturnGame()
    {
        Services.GameManagerService.ReturnGame();
    }

    private async void OnSwitchGamePause(bool isPause)
    {
        if (isPause)
        {
            _controlService.UnlockCursor();
            gameObject.SetActive(true);
            await transform.DOScale(1f, 0.3f).From(0f)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
        }
        else
        {
            _controlService.LockCursor();
            _canvasGroup.interactable = false;
            await transform.DOScale(0f, 0.3f)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }
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

    private void OnQuitDialogSubmitted(bool result)
    {
        if (result)
        {
            //todo EXIT
        }
        else
        {
            //todo close dialog
        }
    }
}