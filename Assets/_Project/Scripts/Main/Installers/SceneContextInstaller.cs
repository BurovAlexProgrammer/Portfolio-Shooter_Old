using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private PlayerBase _playerPrefab;
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private BrainControlService _brainControlServicePrefab;

        public override void InstallBindings()
        {
            InstallPlayer();
            InstallBrainControl();
        }

        private void InstallPlayer()
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

        private void InstallBrainControl()
        {
            Container
                .Bind<BrainControlService>()
                .FromComponentInNewPrefab(_brainControlServicePrefab)
                .WithGameObjectName("BrainControl Service")
                .AsSingle();
        }
    }
}
