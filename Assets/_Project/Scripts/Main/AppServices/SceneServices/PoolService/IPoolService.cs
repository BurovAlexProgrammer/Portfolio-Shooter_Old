using _Project.Scripts.Main.Installers;
using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Main.AppServices.SceneServices.PoolService
{
    public interface IPoolService : IGamePlayContextItem
    {
        public MonoPoolItemBase Get(MonoPoolItemBase prefab);
        public MonoPoolItemBase GetAndActivate(MonoPoolItemBase prefab);
        public void Disable();
    }
}