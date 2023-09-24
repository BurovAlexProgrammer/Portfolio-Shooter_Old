using Main.Contexts;
using Main.Contexts.Installers;
using Main.Game;
using Main.Services;
using UnityEngine;

namespace _Project.Scripts.Main.Contexts.SceneContexts
{
    public class DefaultGameSceneContextInstaller : SceneContextInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private SpawnControlService _spawnControlService;
        [SerializeField] private BrainControlService _brainControlService;
        [SerializeField] private GameUiService _gameUiService;
        

        protected override void InstallBindings()
        {
            Context.Bind<GameUiService>(ContextScope.Scene).FromInstance(_gameUiService);    
            Context.Bind<PoolService>(ContextScope.Scene).FromNew().As<IPoolService>();
            Context.Bind<SpawnControlService>(ContextScope.Scene).FromInstance(_spawnControlService);
            Context.Bind<BrainControlService>(ContextScope.Scene).FromInstance(_brainControlService);
            Context.Bind<Player>(ContextScope.Scene).FromNewPrefab(_player);
        }
    }
}