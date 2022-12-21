using _Project.Scripts.Extension;
using UnityEngine;

namespace _Project.Scripts.Samples
{
    public class InputAction : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        private Controls _controls;
    
        private void Awake()
        {
            _controls = new Controls();
            var _playerControl = _controls.Player;
            _playerControl.Move.BindAction(BindActions.Started, (context => Debug.Log("Started")));
            _playerControl.Move.BindAction(BindActions.Performed, Move);
            _playerControl.Move.BindAction(BindActions.Canceled, (context => Debug.Log("Canceled")));
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Act(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log("Work");
        }

        private void Move(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            var input = ctx.ReadValue<Vector2>();
            _targetTransform.Translate(5 * input.x * Time.deltaTime, 0, 5 * input.y * Time.deltaTime);
        }
    }
} 