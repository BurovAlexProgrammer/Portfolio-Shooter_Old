using System;
using _Project.Scripts.Main.Game.Weapon;
using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Game
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class BasePlayer : MonoBehaviour
    {
        [SerializeField] private CameraHolder _cameraHolder;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private GunBase _gun;
        [SerializeField] private bool _canMove;
        [SerializeField] private bool _canShoot;

        [Inject] private ControlService _controlService;
        [Inject] private SettingsService _settingsService;
        
        private CharacterController _characterController;
        private Controls.PlayerActions _playerControl;
        private Vector2 _moveInputValue;
        private Vector2 _moveLerpValue;
        private Vector2 _rotateInputValue;
        private Vector2 _rotateLerpValue;
        private float _rotationY;
        private bool _shootInputValue;

        public CameraHolder CameraHolder => _cameraHolder;
        
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _playerControl = _controlService.Controls.Player;
        }

        private void Update()
        {
            _rotateInputValue = _playerControl.Rotate.ReadValue<Vector2>();
            _rotateLerpValue = Vector2.Lerp(_rotateLerpValue, _rotateInputValue, _config.RotateLerpTime);
            
            if (_rotateLerpValue != Vector2.zero)
            {
                Rotate(_rotateLerpValue);
            }

            if (_playerControl.Shoot.inProgress)
            {
                TryShoot();
            }
        }

        private void FixedUpdate()
        {
            _moveInputValue = _playerControl.Move.ReadValue<Vector2>().normalized;
            _moveLerpValue = Vector2.Lerp(_moveLerpValue, _moveInputValue, _config.MoveLerpTime);
            
            if (_moveLerpValue != Vector2.zero)
            { 
                Move(_moveLerpValue);
            }
        }

        public void Disable()
        {
            _canMove = false;
            _canShoot = false;
        }

        public void Enable()
        {
            _canMove = true;
            _canShoot = true;
        }
        
        protected virtual void Move(Vector2 inputValue)
        {
            if (!_canMove) return;
            var moveVector = inputValue * Time.deltaTime * _config.MoveSpeed;
            _characterController.Move(transform.right * moveVector.x + transform.forward * moveVector.y);
        } 

        protected virtual void Rotate(Vector2 rotation)
        {
            if (!_canMove) return;

            var delta = Time.deltaTime * _config.RotateSpeed * _settingsService.GameSettings.Sensitivity;
            _rotationY -= rotation.y * delta;
            _rotationY = Math.Clamp(_rotationY, -_config.MaxVerticalAngle, _config.MaxVerticalAngle);
            _cameraHolder.transform.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
            transform.Rotate(rotation.x * Vector3.up * delta);
        }

        protected virtual void TryShoot()
        {
            if (!_canShoot) return;

            Debug.Log("TryShoot");
            _gun.TryShoot();
        }

        protected virtual void Shoot()
        {
            
        }
    }
}