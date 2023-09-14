using System;
using Main.Contexts;
using Main.Game.Player;
using Main.Services;
using UnityEngine;

namespace Main.SceneScripts
{
    public abstract class SceneBehaviourBase : MonoBehaviour, ISceneBehaviour
    {
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private bool _smoothSceneAppearance;
        
        private PlayerBase _player;
        private ScreenService _screenService;

        protected virtual void Awake()
        {
            _screenService = Context.GetService<ScreenService>();
        }

        protected virtual void Start()
        {
            if (_smoothSceneAppearance)
            {
                _screenService.ShowSceneAsync();
            }
            
            _player = Context.GetSceneObject<Player>();

            if (_player != null)
            {
                _player.CameraHolder.SetCamera();
                _player.Enable();
                _player.transform.position = _playerStartPoint.position;
                _player.transform.rotation = _playerStartPoint.rotation;
            }
        }
    }
}