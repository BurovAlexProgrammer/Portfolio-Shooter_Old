using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Window;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services.SceneServices
{
    public class GameUiService : BaseService
    {
        [SerializeField] private BarView _healthBarView;
        [SerializeField] private WindowGamePause _windowGamePause;
        
        [Inject] private ControlService _controlService;
        [Inject] private PlayerBase _player;
        
        private void OnDestroy()
        {
            Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
            _player.Health.Changed -= OnPlayerHealthChanged;
        }

        public void Init()
        {
            Services.GameManagerService.SwitchPause += OnSwitchGamePause;
            _healthBarView.Init(_player.Health.CurrentValue, _player.Health.MaxValue);
            _player.Health.Changed += OnPlayerHealthChanged;
        }
        
        private void OnSwitchGamePause(bool isPause)
        {
            if (isPause)
            {
                _controlService.UnlockCursor();
                _windowGamePause.Show();
            }
            else
            {
                _controlService.LockCursor();
                _windowGamePause.Close();
            }
        }

        private void OnPlayerHealthChanged(HealthBase playerHealth)
        {
            _healthBarView.SetValue(playerHealth.CurrentValue);
        }
    }
}