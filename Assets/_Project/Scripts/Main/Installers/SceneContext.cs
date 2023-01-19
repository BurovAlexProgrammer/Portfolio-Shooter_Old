using System;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public class SceneContext : MonoInstaller
    {
        private static SceneContext _instance;
        public static SceneContext Instance => _instance;
        
        [SerializeField] private PlayerBase _playerPrefab;
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private BrainControlService _brainControlServiceInstance;
        [SerializeField] private SpawnControlService _spawnControlServiceInstance;

        private PlayerBase _player;

        public PlayerBase Player => _player;
        public BrainControlService BrainControl => _brainControlServiceInstance;
        public SpawnControlService SpawnControl => _spawnControlServiceInstance;
        
        
        public override void InstallBindings()
        {
            if (_instance != null) throw new Exception("SceneContext singleton already exists");

            _instance = this;
            
            InstallPlayer();
            InstallBrainControl();
            InstallSpawnControl();
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
                    _player = instance as PlayerBase;
                    var playerTransform = _player.transform;
                    playerTransform.position = _playerStartPoint.position;
                    playerTransform.rotation = _playerStartPoint.rotation;
                });
        }

        private void InstallBrainControl()
        {
            Container
                .Bind<BrainControlService>()
                .FromInstance(_brainControlServiceInstance)
                .AsSingle();
        }

        private void InstallSpawnControl()
        {
            Container
                .Bind<SpawnControlService>()
                .FromInstance(_spawnControlServiceInstance)
                .AsSingle();
        }
    }
}
