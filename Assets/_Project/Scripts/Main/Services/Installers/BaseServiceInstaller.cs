using Main.Extension;
using Main.Contexts;
using smApplication.Scripts.Extension;
using Main.Services;
using UnityEngine;

namespace Main.Services
{
    public abstract class BaseServiceInstaller: MonoBehaviour, IServiceInstaller
    {
        public virtual IServiceInstaller Install()
        {
            var installer = Instantiate(this, Context.ServicesHierarchy);
            installer.gameObject.CleanName();
            return installer;
        }
    }
}