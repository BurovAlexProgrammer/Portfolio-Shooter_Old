using _Project.Scripts.Main.AppServices.Base;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    [UsedImplicitly]
    public class ControlService : ServiceBase
    {
        public readonly Controls Controls;
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