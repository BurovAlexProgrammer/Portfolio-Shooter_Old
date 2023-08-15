using _Project.Scripts.Extension;
using _Project.Scripts.Main.Contexts;
using Main.Contexts;
using smApplication.Scripts.Extension;
using Main.Service;
using UnityEngine;

namespace Main.Service
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