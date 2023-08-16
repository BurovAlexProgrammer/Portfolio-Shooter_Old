using Main.Game.Player;
using Main.Services;

namespace Main.Contexts.Installers
{
    public interface IGamePlayContextInstaller 
    {
        public PlayerBase Player { get; }
        public IPoolService PoolService { get; }
        public BrainControlService BrainControl { get; }
        public SpawnControlService SpawnControl { get; }
    }
}