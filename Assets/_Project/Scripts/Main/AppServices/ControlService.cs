using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    [UsedImplicitly]
    public class ControlService : IService
    {
        public Controls Controls { get; private set; }
        public CursorLockMode CursorLockState => Cursor.lockState;

        public ControlService()
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