using Zenject;

namespace _Project.Scripts.Main.AppServices.Base
{
    public abstract class ServiceBase : IService
    {
        [Inject]
        protected void Construct()
        {
            this.RegisterService();
        }
    }
}