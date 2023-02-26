using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Window;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.AppServices.SceneServices
{
    public class GameUiService : MonoGamePlayContext
    {
        [SerializeField] private BarView _healthBarView;
        [SerializeField] private WindowGamePause _windowGamePause;
        [SerializeField] private WindowGameOver _windowGameOver;
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private TextMeshProUGUI _scoreCountText;

        GameManagerService _gameManager;
        StatisticService _statisticService;
        PlayerBase _player;

        private bool _dialogShowing;

        public bool DialogShowing => _dialogShowing;

        [Inject]
        public void Construct(GameManagerService gameManager, StatisticService statisticService, PlayerBase player)
        {
            _gameManager = gameManager;
            _statisticService = statisticService;
            _player = player;
            Init();
            this.RegisterContext();
        }

        private void OnDestroy()
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