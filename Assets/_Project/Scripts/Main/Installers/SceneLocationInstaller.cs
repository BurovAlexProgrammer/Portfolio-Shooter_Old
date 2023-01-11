using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public class SceneLocationInstaller : MonoInstaller
    {
        [SerializeField] private PlayerBase _playerPrefab;
        [SerializeField] private Transform _playerStartPoint;

        private PlayerBase _playerInstance;

        public PlayerBase PlayerInstance => _playerInstance;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerBase>()
                .FromComponentInNewPrefab(_playerPrefab)
                .WithGameObjectName("Player")
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var playerTransform = (instance as PlayerBase).transform;
                    playerTransform.position = _playerStartPoint.position;
                    playerTransform.rotation = _playerStartPoint.rotation;
                });
        }
    }
}
