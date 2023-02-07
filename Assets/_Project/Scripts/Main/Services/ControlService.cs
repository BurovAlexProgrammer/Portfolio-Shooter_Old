using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class ControlService : BaseService
    {
        public Controls Controls { get; private set; }
        public CursorLockMode CursorLockState => Cursor.lockState;

        private void OnEnable()
        {
            Controls.Enable();
        }

        private void OnDisable()
        {
            Controls.Disable();
        }

        public void Init()
        {
            Controls = new Controls();
        }

        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}