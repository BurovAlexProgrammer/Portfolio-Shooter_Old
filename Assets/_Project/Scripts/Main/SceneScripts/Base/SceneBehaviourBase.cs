using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Game;
using Main.Services;
using UnityEngine;

namespace Main.SceneScripts
{
    public abstract class SceneBehaviourBase : MonoBehaviour, ISceneBehaviour
    {
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private bool _smoothSceneAppearance;
        
        private Player _player;
        private ScreenService _screenService;

        protected virtual void Awake()
        {
            _screenService = Context.Resolve<ScreenService>();
        }

        protected virtual void Start()
        {
            if (_smoothSceneAppearance)
            {
                _screenService.ShowSceneAsync().Forget();
            }
            
            _player = Context.Resolve<Player>();

            if (_player != null)
            {
                _player.CameraHolder.SetCamera();
                _player.Enable();
                _player.transform.position = _playerStartPoint.position;
                _player.transform.rotation = _playerStartPoint.rotation;
            }
        }
    }
}