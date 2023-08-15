using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;
using UnityEngine;

namespace _Project.Scripts.Main.Localizations
{
    public abstract class LocalizedTextComponent : MonoBehaviour
    {
        protected LocalizationService _localization;

        private void Awake()
        {
            _localization = Context.GetService<LocalizationService>();
        }

        private async void Start()
        {
            while (!_localization.IsLoaded)
            {
                await UniTask.NextFrame();
            }
            
            SetText();
        }

        protected abstract void SetText();
    }
}