using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Main.AppServices.PoolService
{
    public interface IPoolService
    {
        public MonoPoolItemBase Get(MonoPoolItemBase prefab);
        public MonoPoolItemBase GetAndActivate(MonoPoolItemBase prefab);
        public void Disable();
    }
}