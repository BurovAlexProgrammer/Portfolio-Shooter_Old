using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.AppServices.Base
{
    public abstract class MonoServiceBase : MonoBehaviour, IService
    {
        [Inject]
        protected virtual void Construct()
        {
            this.RegisterService();
        }
    }
}