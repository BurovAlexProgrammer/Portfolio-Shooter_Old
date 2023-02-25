using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices.Base;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenService : MonoServiceBase
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Volume _volume;
        [SerializeField] private GraphyManager _internalProfiler;
        [SerializeField] private bool _showProfilerOnStartup;

        private ControlService _controlService;

        public Camera MainCamera => _mainCamera;
        public VolumeProfile VolumeProfile => _volume.profile;


        [Inject]
        public void Construct(ControlService controlService)
        {
            _controlService = controlService;
            var controls = _controlService.Controls;
            _internalProfiler.enabled = _showProfilerOnStartup;
            controls.Player.InternalProfiler.BindAction(BindActions.Started, ctx => ToggleShowProfiler());
        }

        private void ToggleShowProfiler()
        {
            _internalProfiler.enabled = !_internalProfiler.enabled;
        }
    }
}