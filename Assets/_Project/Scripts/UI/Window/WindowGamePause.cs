using System;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.UI;

public class WindowGamePause : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Button _returnGameButton;
    [SerializeField] private Button _quitGameButton;

    private void Awake()
    {
        _returnGameButton.onClick.AddListener(ReturnGame);
        Services.GameManagerService.SwitchPause += OnSwitchGamePause;
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

    private void OnSwitchGamePause(bool isPause)
    {
        gameObject.SetActive(isPause);
    }
}
