using Main.Game;
using Main.Services;

namespace Main.Contexts.Installers
{
    public interface IGamePlayContextInstaller 
    {
        public Player Player { get; }
        public IPoolService PoolService { get; }
        public BrainControlService BrainControl { get; }
        public SpawnControlService SpawnControl { get; }
    }
}