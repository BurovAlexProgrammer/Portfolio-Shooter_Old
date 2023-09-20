using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main.SceneScripts
{
    public abstract class SceneBehaviourBase : MonoBehaviour
    {
        [SerializeField] protected bool _smoothSceneAppearance;
        
        protected ScreenService _screenService;

        protected virtual void Awake()
        {
            _screenService = Context.Resolve<ScreenService>();
        }

        protected virtual void Start()
        {
            if (_smoothSceneAppearance)
            {
                _screenService.ScreenFadeInAsync().Forget();
            }
        }
    }
}