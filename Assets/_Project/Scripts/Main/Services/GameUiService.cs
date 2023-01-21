using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Window;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameUiService : BaseService
    {
        [SerializeField] private BarView _healthBarView;
        [SerializeField] private WindowGamePause _windowGamePause;
        
        [Inject] private ControlService _controlService;

        private void OnDestroy()
        {
            Services.GameManagerService.SwitchPause -= OnSwitchGamePause;
        }

        public void Init()
        {
            Services.GameManagerService.SwitchPause += OnSwitchGamePause;
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
    }
}