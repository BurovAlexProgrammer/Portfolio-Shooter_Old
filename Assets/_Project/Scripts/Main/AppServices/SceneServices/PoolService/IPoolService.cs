using _Project.Scripts.Main.Installers;
using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Main.AppServices.SceneServices.PoolService
{
    public interface IPoolService : IGamePlayContextItem
    {
        public BasePoolItem Get(BasePoolItem prefab);
        public BasePoolItem GetAndActivate(BasePoolItem prefab);
        public void Disable();
    }
}