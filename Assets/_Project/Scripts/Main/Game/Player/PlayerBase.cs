using System;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Audio;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.Game.Weapon;
using Main.Contexts;
using Main.Service;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    [RequireComponent(typeof(HealthBase))]
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class PlayerBase : MonoBehaviour, IPlayer
    {
        [SerializeField] private CameraHolder _cameraHolder;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private GunBase _gun;
        [SerializeField] private bool _canMove;
        [SerializeField] private bool _canRotate;
        [SerializeField] private bool _canShoot;
        [SerializeField] private bool _useGravity;
        [SerializeField] private SimpleAudioEvent _startPhrase;

        private StatisticService _statisticService;
        private ControlService _controlService;
        private SettingsService _settingsService;

        private UnityEngine.CharacterController _characterController;
        private AudioSource _audioSource;
        private HealthBase _health;
        private Controls.PlayerActions _playerControl;
        private Vector2 _moveInputValue;
        private Vector2 _moveLerpValue;
        private Vector2 _rotateInputValue;
        private Vector2 _rotateLerpValue;
        private float _rotationY;
        private bool _shootInputValue;
        private Vector3 _playerMove;

        public CameraHolder CameraHolder => _cameraHolder;
        public HealthBase Health => _health;

        private void Awake()
        {
            _statisticService = Context.GetService<StatisticService>();
                _controlService = Context.GetService<ControlService>();
            _settingsService = Context.GetService<SettingsService>();
            _health = GetComponent<HealthBase>();
            _characterController = GetComponent<UnityEngine.CharacterController>();
            _audioSource = GetComponent<AudioSource>();
            _playerControl = _controlService.Controls1.Player;
        }

        private void Start()
        {
            if (_startPhrase != null)
            {
                _startPhrase.Play(_audioSource);
            }
            else
            {
                Debug.LogWarning("Player does not have Start Phrase. (Click to select)", this);
            }
        }

        private void Update()
        {
            _rotateInputValue = _playerControl.Rotate.ReadValue<Vector2>();
            _rotateLerpValue = Vector2.Lerp(_rotateLerpValue, _rotateInputValue, _config.RotateLerpTime);

            if (_canRotate)
            {
                Rotate(_rotateLerpValue);
            }

            if (_canShoot && _playerControl.Shoot.IsPressed())
            {
                TryShoot();
            }
        }

        private void FixedUpdate()
        {
            _moveInputValue = _playerControl.Move.ReadValue<Vector2>().normalized;
            _moveLerpValue = Vector2.Lerp(_moveLerpValue, _moveInputValue, _config.MoveLerpTime);

            if (_canMove)
            {
                Move(_moveLerpValue);
            }
        }

        public void Disable()
        {
            _canMove = false;
            _canRotate = false;
            _canShoot = false;
        }

        public void Enable()
        {
            _canMove = true;
            _canRotate = true;
            _canShoot = true;
        }

        public virtual void Move(Vector2 inputValue)
        {
            if (!_canMove) return;

            var moveVector = inputValue * Time.fixedDeltaTime * _config.MoveSpeed;
            var gravityVelocity = _useGravity ? Physics.gravity * Time.fixedDeltaTime : Vector3.zero;
            _characterController.Move(transform.right * moveVector.x + transform.forward * moveVector.y +
                                      gravityVelocity);

            if (_characterController.velocity != Vector3.zero)
            {
                _statisticService.AddValueToRecord(StatisticData.RecordName.Movement,
                    _characterController.velocity.magnitude * Time.fixedDeltaTime);
            }
        }

        public virtual void Rotate(Vector2 inputValue)
        {
            if (!_canMove) return;
            if (inputValue == Vector2.zero) return;

            var delta = Time.deltaTime * _config.RotateSpeed * _settingsService.GameSettings.Sensitivity;
            _rotationY -= inputValue.y * delta;
            _rotationY = Math.Clamp(_rotationY, -_config.MaxVerticalAngle, _config.MaxVerticalAngle);
            _cameraHolder.transform.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
            transform.Rotate(inputValue.x * Vector3.up * delta);
        }

        protected virtual void TryShoot()
        {
            if (_gun.TryShoot())
            {
                _statisticService.AddValueToRecord(StatisticData.RecordName.FireCount, 1);
            }
        }
    }
}