using System.Runtime.CompilerServices;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.AppServices.SceneServices;
using _Project.Scripts.Main.AppServices.SceneServices.PoolService;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Installers;

namespace _Project.Scripts.Main.Contexts
{
    public static class GamePlayContext
    {
        private static readonly ServiceLocator<IGamePlayContextItem> _contextLocator = new();
        public static GameUiService GameUiService { get; private set; }
        public static PlayerBase Player { get; private set; }
        public static IPoolService PoolService { get; private set; }
        public static SpawnControlService Spawner { get; private set; }
        public static BrainControlService BrainControlService { get; private set; }

        private static T Get<T>() where T : IGamePlayContextItem => _contextLocator.Get<T>();

        public static void RegisterContext<T>(this T instance) where T : IGamePlayContextItem
        {
            _contextLocator.Register(instance);

            switch (instance)
            {
                case GameUiService service:
                    GameUiService = service;
                    break;
                case PlayerBase service:
                    Player = service;
                    break;
                case IPoolService service:
                    PoolService = service;
                    break;
                case BrainControlService service:
                    BrainControlService = service;
                    break;
                case SpawnControlService service:
                    Spawner = service;
                    break;
                default:
                    throw new SwitchExpressionException();
            }
        }
    }
}