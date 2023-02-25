using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.SceneScripts
{
    public abstract class SceneBehaviourBase : MonoInstaller, ISceneBehaviour
    {
        [SerializeField] private Transform _playerStartPoint;
        private PlayerBase _player;
        
        public override void Start()
        {
            Debug.Log("Start");
            _player = GamePlayContext.Player;
            _player.CameraHolder.SetCamera();
            _player.Enable();
            _player.transform.position = _playerStartPoint.position;
            _player.transform.rotation = _playerStartPoint.rotation;
        }

        public override void InstallBindings()
        {
        }
    }
}