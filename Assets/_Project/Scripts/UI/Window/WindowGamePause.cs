using System;
using _Project.Scripts.Main.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WindowGamePause : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Button _returnGameButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _returnGameButton.onClick.AddListener(ReturnGame);
        Services.GameManagerService.SwitchPause += OnSwitchGamePause;
        _canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _returnGameButton.onClick.RemoveAllListeners();
        Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
    }

    private void ReturnGame()
    {
        Services.GameManagerService.ReturnGame();
    }

    private async void OnSwitchGamePause(bool isPause)
    {
        if (isPause)
        {
            gameObject.SetActive(true);
            await transform.DOScale(1f, 0.3f).From(0f).AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
        }
        else
        {
            _canvasGroup.interactable = false;
            await transform.DOScale(0f, 0.3f).AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }
    }
}