using System;
using Main.Contexts.DI;
using Main.Settings;
using sm_application.Scripts.Main.Wrappers;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Main.Services
{
    public class ScreenService : MonoBehaviour, IService
    {
        [FormerlySerializedAs("_mainCamera")] [SerializeField] private Camera _cameraMain;
        [SerializeField] private Volume _volume;
        [SerializeField] private GraphyManager _internalProfiler;
        [SerializeField] private Toggle _internalProfilerToggle;
        [SerializeField] private Transform _cameraHolder;
        
        [SerializeField] private bool _showProfilerOnStartup;
        
        [Inject] private ControlService _controlService;

        public Camera CameraMain => _cameraMain;
        public VolumeProfile VolumeProfile => _volume.profile;
        
        public Action<bool> OnDebugProfilerToggleSwitched; 

        public void Construct()
        {
            _internalProfilerToggle.isOn = _internalProfiler.gameObject.activeSelf;
            _internalProfilerToggle.onValueChanged.AddListener(OnProfilerToggleSwitched);
            _internalProfilerToggle.isOn = _showProfilerOnStartup;
        }
        
        private void OnProfilerToggleSwitched(bool value)
        {
            OnDebugProfilerToggleSwitched?.Invoke(value);
        }

        public void Dispose()
        {
            _internalProfilerToggle.onValueChanged.RemoveListener(OnProfilerToggleSwitched);
            GC.SuppressFinalize(this);
        }
        
        public void ActiveProfileVolume<T>(bool active) where T : IPostProcessComponent
        {
            var type = typeof(T);
            if (_volume.profile.TryGet(type, out VolumeComponent volumeComponent))
            {
                volumeComponent.active = active;
                return;
            }

            throw new Exception($"VolumeProfile {type.FullName} not found.");
        }
        
        public void SetCameraPlace(Transform parent)
        {
            Log.Info("Camera was moved to cameraHolder (Click to select CameraHolder)", parent);
            var mainCameraTransform = _cameraMain.transform;
            mainCameraTransform.parent = parent;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void ReturnCameraToService()
        {
            var mainCameraTransform = _cameraMain.transform;
            Log.Info("Camera was moved to ScreenService", _cameraHolder);
            mainCameraTransform.parent = _cameraHolder;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void SetCameraToCanvas(Canvas canvas)
        {
            canvas.worldCamera = _cameraMain;
        }

        public void ApplySettings(VideoSettings videoSettings)
        {
            ActiveProfileVolume<Bloom>(videoSettings.PostProcessBloom);
            ActiveProfileVolume<DepthOfField>(videoSettings.PostProcessDepthOfField);
            ActiveProfileVolume<Vignette>(videoSettings.PostProcessVignette);
            ActiveProfileVolume<FilmGrain>(videoSettings.PostProcessFilmGrain);
            ActiveProfileVolume<MotionBlur>(videoSettings.PostProcessMotionBlur);
            ActiveProfileVolume<LensDistortion>(videoSettings.PostProcessLensDistortion);
            var additionalCameraSettings = _cameraMain.GetComponent<UniversalAdditionalCameraData>();
            additionalCameraSettings.antialiasing = videoSettings.PostProcessAntiAliasing ? AntialiasingMode.FastApproximateAntialiasing : AntialiasingMode.None;
            //var t1 = GraphicsSettings.GetGraphicsSettings();
            //GraphicsSettings.GetSettingsForRenderPipeline<>()
        }

        public void SetupInternalProfiler(AudioListener audioListener)
        {
            _internalProfiler.AudioListener = audioListener;
        }
        
        public void SetAudioListenerToCamera(AudioListener audioListener)
        {
           audioListener.transform.SetParent(_cameraMain.transform);
           audioListener.transform.localPosition = Vector3.zero;
        }
    }
}