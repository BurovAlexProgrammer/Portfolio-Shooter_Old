using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Main.Services
{
    [UsedImplicitly]
    [RequireComponent(typeof(EventSystem), typeof(InputSystemUIInputModule))]
    public class ControlService : MonoBehaviour, IService, IConstruct
    {
        public Controls _controls;
        public CursorLockMode CursorLockState => Cursor.lockState;

        public Controls Controls => _controls;

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