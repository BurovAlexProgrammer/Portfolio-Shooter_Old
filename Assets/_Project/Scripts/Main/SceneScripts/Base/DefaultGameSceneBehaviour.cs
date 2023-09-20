using Main.Contexts;
using Main.Game;
using Main.Services;
using UnityEngine;

namespace Main.SceneScripts
{
    public class DefaultGameSceneBehaviour : SceneBehaviourBase
    {
        [SerializeField] private Transform _playerStartPoint;

        private Player _player;
        private SpawnControlService _spawnControlService;
        private BrainControlService _brainControlService;
        
        protected override void Start()
        {
            base.Start();
            
            _player = Context.Resolve<Player>();
            _spawnControlService = Context.Resolve<SpawnControlService>();
            _brainControlService = Context.Resolve<BrainControlService>();

            if (_player != null)
            {
                _player.CameraHolder.SetCamera();
                _player.Enable();
                _player.transform.position = _playerStartPoint.position;
                _player.transform.rotation = _playerStartPoint.rotation;
            }
            
            _spawnControlService.StartSpawn();
        }
    }
}