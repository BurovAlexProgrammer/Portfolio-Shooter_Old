using Cysharp.Threading.Tasks;
using Main.Game;
using Main.Game.Health;
using Main.UI;
using Main.UI.Window;
using TMPro;

namespace Main.Services
{
    public class GameUiService : IService
    {
        private BarView _healthBarView;
        private WindowGamePause _windowGamePause;
        private WindowGameOver _windowGameOver;
        private TextMeshProUGUI _killCountText;
        private TextMeshProUGUI _scoreCountText;

        GameManagerService _gameManager;
        StatisticService _statisticService;
        Player _player;

        private bool _dialogShowing;

        public bool DialogShowing => _dialogShowing;

        ~GameUiService()
        {
            _gameManager.SwitchPause -= OnSwitchGamePause;
            _gameManager.GameOver -= OnGameOver;
            _player.Health.OnChanged -= OnPlayerHealthChanged;
            _statisticService.RecordChanged -= OnStaticRecordChanged;
            _windowGamePause.DialogSwitched -= OnDialogSwitched;
            _windowGameOver.DialogSwitched -= OnDialogSwitched;
        }

        private void OnDialogSwitched(bool state)
        {
            _dialogShowing = state;
        }

        private void Init()
        {
            _gameManager.SwitchPause += OnSwitchGamePause;
            _gameManager.GameOver += OnGameOver;
            _player.Health.OnChanged += OnPlayerHealthChanged;
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