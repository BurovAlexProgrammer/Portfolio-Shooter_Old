// using _Project.Scripts.Main.AppServices.SceneServices;
// using _Project.Scripts.Main.AppServices.SceneServices.PoolService;
// using _Project.Scripts.Main.Game;
// using sm_application.Scripts.Main.Service;
// using UnityEngine;
//
// namespace _Project.Scripts.Main.Contexts.Installers
// {
//     public class GamePlayContextInstaller : MonoInstaller
//     {
//         [SerializeField] private PlayerBase _playerPrefab;
//         [SerializeField] private GameUiService _gameUiServicePrefab;
//         [SerializeField] private BrainControlService _brainControlServiceInstance;
//         [SerializeField] private SpawnControlService _spawnControlServiceInstance;
//
//         public override void InstallBindings()
//         {
//             GamePlayContext.Clear();
//             InstallPlayer();
//             InstallGameUI();
//             InstallPoolService();
//             InstallBrainControl();
//             InstallSpawnControl();
//             GamePlayContext.RegisterContext<IPoolService>(typeof(PoolService));
//             GamePlayContext.RegisterNewPrefab<GameUiService>(_gameUiServicePrefab);
//             GamePlayContext.RegisterContext(_brainControlServiceInstance);
//             GamePlayContext.RegisterContext(_spawnControlServiceInstance);
//             GamePlayContext.RegisterNewPrefab<PlayerBase>(_playerPrefab);
//         }
//         
//         private void OnDestroy()
//         {
//             Container.Unbind<GameUiService>();
//             Container.Unbind<PlayerBase>();
//             Container.Unbind<IPoolService>();
//             Container.Unbind<BrainControlService>();
//             Container.Unbind<SpawnControlService>();
//         }
//         
//         private void InstallPoolService()
//         {
//             Container
//                 .Bind<IPoolService>()
//                 .To<PoolService>()
//                 .FromNew()
//                 .AsSingle()
//                 .NonLazy();
//         }
//
//         private void InstallGameUI()
//         {
//             Container
//                 .Bind<GameUiService>()
//                 .FromComponentInNewPrefab(_gameUiServicePrefab)
//                 .WithGameObjectName("Game UI Service")
//                 .AsSingle()
//                 .NonLazy();
//         }
//
//         private void InstallPlayer()
//         {
//             Container
//                 .Bind<PlayerBase>()
//                 .FromComponentInNewPrefab(_playerPrefab)
//                 .WithGameObjectName("Player")
//                 .AsSingle()
//                 .NonLazy();
//         }
//
//         private void InstallBrainControl()
//         {
//             Container
//                 .Bind<BrainControlService>()
//                 .FromInstance(_brainControlServiceInstance)
//                 .AsSingle().NonLazy(); 
//         }
//
//         private void InstallSpawnControl()
//         {
//             Container
//                 .Bind<SpawnControlService>()
//                 .FromInstance(_spawnControlServiceInstance)
//                 .AsSingle().NonLazy();
//         }
//     }
// }
