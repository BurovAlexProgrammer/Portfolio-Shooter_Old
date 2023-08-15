using _Project.Scripts.Extension;
using Main.Contexts;
using Main.Service;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenService : MonoBehaviour, IService, IConstructInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Volume _volume;
        [SerializeField] private GraphyManager _internalProfiler;
        [SerializeField] private bool _showProfilerOnStartup;

        private ControlService _controlService;

        public Camera MainCamera => _mainCamera;
        public VolumeProfile VolumeProfile => _volume.profile;

        private void Construct()
        {
            _controlService = Context.GetService<ControlService>();
            var controls = _controlService.Controls1;
            _internalProfiler.enabled = _showProfilerOnStartup;
            controls.Player.InternalProfiler.BindAction(BindActions.Started, ctx => ToggleShowProfiler());
        }

        private void ToggleShowProfiler()
        {
            _internalProfiler.enabled = !_internalProfiler.enabled;
        }

        public void Construct(IServiceInstaller installer)
        {
            var serviceInstaller = installer as ScreenServiceInstaller;
            _mainCamera = serviceInstaller.CameraMain;
            _volume = serviceInstaller.Volume;
            _internalProfiler = serviceInstaller.InternalProfilerManager;
            _showProfilerOnStartup = serviceInstaller.ShowProfilerOnStartup;
        }
    }
}