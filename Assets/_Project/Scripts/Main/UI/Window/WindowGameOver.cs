using _Project.Scripts.Main.Services;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Main.UI.Window
{
    public class WindowGameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killsCountText;
        [SerializeField] private TextMeshProUGUI _surviveTimeText;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _quitGameButton;
        [SerializeField] private DialogView _quitGameDialog;
        [SerializeField] private DialogView _returnMainMenuDialog;

        [Inject] private GameManagerService _gameManager;
        
        private void ShowQuitGameDialog()
        {
            _ = _quitGameDialog.Show();
        }

        private void ShowReturnMainMenuDialog()
        {
            _ = _returnMainMenuDialog.Show();
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

        private void OnReturnMainMenuResult(bool result)
        {
            if (result)
            {
                _gameManager.ReturnMainMenu();
                return;
            }

            _ = _returnMainMenuDialog.Close();
        } 
    }
}
