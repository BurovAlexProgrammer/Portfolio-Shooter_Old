using _Project.Scripts.Extension;
using _Project.Scripts.Main.Services;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static _Project.Scripts.Extension.Common;
using static _Project.Scripts.Main.StatisticData.FormatType;
using static _Project.Scripts.Main.StatisticData.RecordName;

namespace _Project.Scripts.Main.UI.Window
{
    public class WindowGameOver : WindowView
    {
        [SerializeField] private TextMeshProUGUI _killsCountText;
        [SerializeField] private TextMeshProUGUI _surviveTimeText;
        [SerializeField] private RectTransform _buttonPanel;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _quitGameButton;
        [SerializeField] private DialogView _quitGameDialog;

        [Inject] private GameManagerService _gameManager;
        [Inject] private StatisticService _statisticService;

        private void Awake()
        {
            _retryButton.onClick.AddListener(Retry);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _quitGameButton.onClick.AddListener(ShowQuitGameDialog);
            _quitGameDialog.Confirm += OnQuitDialogConfirm;
        }

        private void OnDestroy()
        {
            _retryButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _quitGameButton.onClick.RemoveAllListeners();
        }

        private async void Retry()
        {
            await Close();
            _gameManager.RestartGame();
        }

        public override async UniTask Show()
        {
            _buttonPanel.localScale = _buttonPanel.localScale.SetAsNew(x: 0f);
            _buttonPanel.SetScale(x: 0f);
            const float duration = 0.8f;
            var kills = _statisticService.GetIntegerValue(KillMonsterCount, Session);
            var surviveTime =
                Mathf.RoundToInt(_statisticService.GetFloatValue(LastGameSessionDuration, Session));
            await base.Show();


            await DOVirtual
                .Int(0, surviveTime, duration, x => _surviveTimeText.text = x.Format(StringFormat.Time))
                .AsyncWaitForCompletion();
            
            await DOVirtual
                .Int(0, kills, duration, x => _killsCountText.text = x.ToString())
                .AsyncWaitForCompletion();

            await DOVirtual
                .Float(0, 1f, 0.5f, x => _buttonPanel.SetScale(x: x))
                .AsyncWaitForCompletion();
        }

        private async void GoToMainMenu()
        {
            await Close();
            _gameManager.GoToMainMenu();
        } 

        private void ShowQuitGameDialog()
        {
            _quitGameDialog.Show().Forget();
        }

        private void OnQuitDialogConfirm(bool result)
        {
            if (result)
            {
                _gameManager.QuitGame();
                return;
            }
        
            _quitGameDialog.Close().Forget();
        }
    }
}
