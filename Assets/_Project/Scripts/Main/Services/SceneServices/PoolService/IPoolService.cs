using System;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;

namespace Main.Services
{
    public interface IPoolService: IService, IConstruct
    {
        void Init();
        PoolItem GetAndActivate(object prefab);
        Pool CreatePool(object objectRef, int initialCapacity = 1, int maxCapacity = 20, Pool.OverAllocationBehaviour behaviour = Pool.OverAllocationBehaviour.Warning);
        PoolItem Get(object objectKey);
        void Reset();
        void ReturnItem(UInt64 id);
    }
}