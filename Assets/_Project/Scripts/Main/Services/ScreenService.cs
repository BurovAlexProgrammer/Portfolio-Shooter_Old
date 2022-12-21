using _Project.Scripts.Extension;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class ScreenService : BaseService
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private PostProcessVolume _postProcessVolume;
        [SerializeField] private PostProcessLayer _postProcessLayer;
        [SerializeField] private GraphyManager _internalProfiler;
        [SerializeField] private bool _showProfilerOnStartup;

        [Inject] private ControlService _controlService;
        
        public Camera MainCamera => _mainCamera;
        public Camera UICamera => _uiCamera;
        public PostProcessVolume PostProcessVolume => _postProcessVolume;
        public PostProcessLayer PostProcessLayer => _postProcessLayer;

        private void Awake()
        {
            var controls = _controlService.Controls;
            _internalProfiler.enabled = _showProfilerOnStartup;
            controls.Player.InternalProfiler.BindAction(BindActions.Started, ctx => ToggleShowProfiler());
        }

        private void ToggleShowProfiler()
        {
            Debug.Log("VAR");
            _internalProfiler.enabled = !_internalProfiler.enabled;
        }
    }
}