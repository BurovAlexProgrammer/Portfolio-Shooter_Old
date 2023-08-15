using _Project.Scripts.Main.AppServices;
using Main.Contexts;
using Main.Service;
using UnityEngine;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField] private CameraTypes _camera;

        private ScreenService _screenService;

        private void Awake()
        {
            _screenService = Context.GetService<ScreenService>();
        }

        private enum CameraTypes
        {
            MainCamera,
            UiCamera
        }

        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = _camera == CameraTypes.MainCamera ? _screenService.MainCamera : null;
            enabled = false;
        }
    }
}