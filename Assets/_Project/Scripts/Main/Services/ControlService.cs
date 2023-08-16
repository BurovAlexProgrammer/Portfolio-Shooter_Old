using Main.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace Main.Services
{
    [UsedImplicitly]
    public class ControlService : IService, IConstruct
    {
        public Controls _controls;
        public CursorLockMode CursorLockState => Cursor.lockState;

        public Controls Controls1 => _controls;

        public void Construct()
        {
            _controls = new Controls();
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