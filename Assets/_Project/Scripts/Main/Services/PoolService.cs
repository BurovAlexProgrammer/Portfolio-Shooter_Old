using _Project.Scripts.Main.Game.Weapon;
using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Main.Services
{
    public class PoolService : BaseService
    {
        public Destruction _destructionPrefab;
        public MonoPool<Destruction> _destructionPool;

        public void Init()
        {
            _destructionPool = new MonoPool<Destruction>(_destructionPrefab, transform, 10, 20);
        }
    }
}