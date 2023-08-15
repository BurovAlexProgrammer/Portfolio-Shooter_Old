using _Project.Scripts.Main.AppServices.Base;
using JetBrains.Annotations;
using Main.Service;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
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