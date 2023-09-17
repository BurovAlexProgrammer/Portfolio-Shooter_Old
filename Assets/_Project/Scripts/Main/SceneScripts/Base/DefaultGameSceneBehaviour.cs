using Main.Contexts;
using Main.Game;
using UnityEngine;

namespace Main.SceneScripts
{
    public class DefaultGameSceneBehaviour : SceneBehaviourBase
    {
        [SerializeField] private Transform _playerStartPoint;

        private Player _player;
        
        protected override void Start()
        {
            base.Start();
            
            _player = Context.Resolve<Player>();

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