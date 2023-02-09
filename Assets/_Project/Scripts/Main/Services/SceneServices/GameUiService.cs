using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Window;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services.SceneServices
{
    public class GameUiService : BaseService
    {
        [SerializeField] private BarView _healthBarView;
        [SerializeField] private WindowGamePause _windowGamePause;
        [SerializeField] private WindowGameOver _windowGameOver;
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private TextMeshProUGUI _scoreCountText;

        [Inject] private GameManagerService _gameManager;
        [Inject] private StatisticService _statisticService;
        [Inject] private PlayerBase _player;

        private bool _dialogShowing;

        public bool DialogShowing => _dialogShowing;
        
        private void OnDestroy()
        {
            Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
            Services.GameManagerService.GameOver -= OnGameOver;
            _player.Health.Changed -= OnPlayerHealthChanged;
            _statisticService.RecordChanged -= OnStaticRecordChanged;
            _windowGamePause.DialogSwitched -= OnDialogSwitched;
            _windowGameOver.DialogSwitched -= OnDialogSwitched;
        }

        private void OnDialogSwitched(bool state)
        {
            _dialogShowing = state;
        }

        public void Init()
        {
            Services.GameManagerService.SwitchPause += OnSwitchGamePause;
            Services.GameManagerService.GameOver += OnGameOver;
            _player.Health.Changed += OnPlayerHealthChanged;
            _statisticService.RecordChanged += OnStaticRecordChanged;
            _healthBarView.Init(_player.Health.CurrentValue, _player.Health.MaxValue);
            _windowGamePause.DialogSwitched += OnDialogSwitched;
            _windowGameOver.DialogSwitched += OnDialogSwitched;
        }

        private void OnGameOver()
        {
            _windowGameOver.Show().Forget();
        }

        private void OnSwitchGamePause(bool isPause)
        {
            if (_gameManager.IsGameOver) return;
            
            if (isPause)
            {
                _windowGamePause.Show().Forget();
            }
            else
            {
                _windowGamePause.Close().Forget();
            }
        }

        private void OnPlayerHealthChanged(HealthBase playerHealth)
        {
            _healthBarView.SetValue(playerHealth.CurrentValue);
        }

        private void OnStaticRecordChanged(StatisticData.RecordName recordName, string value)
        {
            switch (recordName)
            {
                case StatisticData.RecordName.KillMonsterCount:
                    _killCountText.text = value;
                    break;
                case StatisticData.RecordName.Scores:
                    _scoreCountText.text = value;
                    break;
            }
        }
    }
}