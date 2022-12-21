using UnityEngine.PlayerLoop;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Services
{
    public class ControlService : BaseService
    {
        public Controls Controls { get; private set; }

        public void Init()
        {
            SetService(this);
            Controls = new Controls();
        }

        private void OnEnable()
        {
            Controls.Enable();
        }

        private void OnDisable()
        {
            Controls.Disable();
        }
    }
}