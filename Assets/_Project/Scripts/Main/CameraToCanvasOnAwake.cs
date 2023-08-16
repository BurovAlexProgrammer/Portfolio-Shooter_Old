using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main
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