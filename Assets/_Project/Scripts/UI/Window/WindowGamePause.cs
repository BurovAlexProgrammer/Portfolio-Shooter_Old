using System;
using _Project.Scripts.Main.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowGamePause : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Button _returnGameButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Inject] private SettingsService _settingsService;
    [Inject] private ControlService _controlService;
    
    private void Awake()
    {
        Services.GameManagerService.SwitchPause += OnSwitchGamePause;
        _returnGameButton.onClick.AddListener(ReturnGame);
        _musicToggle.onValueChanged.AddListener(OnMusicSwitch);
        _canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        _musicToggle.isOn = _settingsService.Audio.MusicEnabled;
    }

    private void OnDestroy()
    {
        Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
        _returnGameButton.onClick.RemoveAllListeners();
        _musicToggle.onValueChanged.RemoveAllListeners();
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
}