using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Window;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services.SceneServices
{
    public class GameUiService : BaseService
    {
        [SerializeField] private BarView _healthBarView;
        [SerializeField] private WindowGamePause _windowGamePause;
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private TextMeshProUGUI _scoreCountText;

        [Inject] private StatisticService _statisticService;
        [Inject] private PlayerBase _player;
        
        private void OnDestroy()
        {
            Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
            _player.Health.Changed -= OnPlayerHealthChanged;
            _statisticService.RecordChanged -= OnStaticRecordChanged;
        }

        public void Init()
        {
            Services.GameManagerService.SwitchPause += OnSwitchGamePause;
            _player.Health.Changed += OnPlayerHealthChanged;
            _statisticService.RecordChanged += OnStaticRecordChanged;
            _healthBarView.Init(_player.Health.CurrentValue, _player.Health.MaxValue);
        }
        
        private void OnSwitchGamePause(bool isPause)
        {
            if (isPause)
            {
                _windowGamePause.Show();
            }
            else
            {
                _ = _windowGamePause.Close();
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