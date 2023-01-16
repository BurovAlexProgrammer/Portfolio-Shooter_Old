using System;
using _Project.Scripts.Extension;
using UnityEngine;
using UnityEngine.InputSystem;
using static _Project.Scripts.Main.Services.Services;

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
            SetService(this);
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