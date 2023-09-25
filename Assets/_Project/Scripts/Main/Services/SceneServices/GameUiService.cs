using System;
using Cysharp.Threading.Tasks;
using Main.Contexts.DI;
using Main.Game;
using Main.Game.Health;
using Main.UI;
using Main.UI.Window;
using TMPro;
using UnityEngine;

namespace Main.Services
{
    public class GameUiService : MonoBehaviour, IConstruct, IDisposable
    {
        [SerializeField] private BarView _healthBarView;
        [SerializeField] private WindowGamePause _windowGamePause;
        [SerializeField] private WindowGameOver _windowGameOver;
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private TextMeshProUGUI _scoreCountText;

        [Inject] GameManagerService _gameManager;
        [Inject] StatisticService _statisticService;
        [Inject] Player _player;

        private bool _dialogShowing;

        public bool DialogShowing => _dialogShowing;

        public void Construct()
        {
            _gameManager.SwitchPause += OnSwitchGamePause;
            _gameManager.GameOver += OnGameOver;
            _player.Health.OnChanged += OnPlayerHealthChanged;
            _statisticService.RecordChanged += OnStaticRecordChanged;
            _healthBarView.Init(_player.Health.CurrentValue, _player.Health.MaxValue);
            _windowGamePause.DialogSwitched += OnDialogSwitched;
            _windowGameOver.DialogSwitched += OnDialogSwitched;
        }
        
        public void Dispose()
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