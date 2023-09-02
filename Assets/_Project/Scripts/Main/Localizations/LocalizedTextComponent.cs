using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main.Localizations
{
    public abstract class LocalizedTextComponent : MonoBehaviour
    {
        protected LocalizationService _localization;

        protected virtual void Awake()
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