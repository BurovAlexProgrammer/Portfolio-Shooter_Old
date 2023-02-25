using _Project.Scripts.Main.AppServices.SceneServices;
using _Project.Scripts.Main.AppServices.SceneServices.PoolService;
using _Project.Scripts.Main.Game;

namespace _Project.Scripts.Main.Contexts.Installers
{
    public interface IGamePlayContextInstaller 
    {
        public PlayerBase Player { get; }
        public IPoolService PoolService { get; }
        public BrainControlService BrainControl { get; }
        public SpawnControlService SpawnControl { get; }
    }
}