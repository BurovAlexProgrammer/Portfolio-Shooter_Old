using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public class SceneLocationInstaller : MonoInstaller
    {
        [SerializeField] private BasePlayer _playerPrefab;
        [SerializeField] private Transform _playerStartPoint;
        public override void InstallBindings()
        {
            Container
                .Bind<BasePlayer>()
                .FromComponentInNewPrefab(_playerPrefab)
                .WithGameObjectName("Player")
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var playerTransform = (instance as BasePlayer).transform;
                    playerTransform.position = _playerStartPoint.position;
                    playerTransform.rotation = _playerStartPoint.rotation;
                });
        }
    }
}
