using System;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.AppServices.SceneServices;
using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;
using static _Project.Scripts.Main.AppServices.Services;

namespace _Project.Scripts.Main.Installers
{
    public class GameContext : MonoInstaller
    {
        private static GameContext _instance;
        public static GameContext Instance => _instance;
        
        [SerializeField] private PlayerBase _playerPrefab;
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private GameUiService _gameUiServicePrefab;
        [SerializeField] private PoolService _poolServicePrefab;
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
            InstallGameUI();
            InstallPoolService();
            InstallBrainControl();
            InstallSpawnControl();
        }
        
        private void OnDestroy()
        {
            KillService(_poolServicePrefab);
            KillService(_brainControlServiceInstance);
            KillService(_spawnControlServiceInstance);
            Container.Unbind<GameUiService>();
            Container.Unbind<Player>();
            Container.Unbind<BrainControlService>();
            Container.Unbind<SpawnControlService>();
        }
        
        private void InstallPoolService()
        {
            Container
                .Bind<PoolService>()
                .FromComponentInNewPrefab(_poolServicePrefab)
                .WithGameObjectName("Pool Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var service = instance as PoolService;
                    service.Init();
                    SetService(service);
                })
                .NonLazy();
        }

        private void InstallGameUI()
        {
            Container
                .Bind<GameUiService>()
                .FromComponentInNewPrefab(_gameUiServicePrefab)
                .WithGameObjectName("Game UI Service")
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var service = instance as GameUiService;
                    service.Init();
                })
                .NonLazy();
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
                    var playerTransform = _player.Transform;
                    playerTransform.position = _playerStartPoint.position;
                    playerTransform.rotation = _playerStartPoint.rotation;
                    Destroy(_playerStartPoint.gameObject);
                });
        }

        private void InstallBrainControl()
        {
            Container
                .Bind<BrainControlService>()
                .FromInstance(_brainControlServiceInstance)
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var service = instance as BrainControlService;
                    SetService(service);
                })
                .NonLazy(); 
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
