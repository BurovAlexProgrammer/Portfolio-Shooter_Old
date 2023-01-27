using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField] private CameraTypes _camera;

        [Inject] private ScreenService _screenService;

        private enum CameraTypes
        {
            MainCamera,
            UiCamera
        }

        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = _camera == CameraTypes.MainCamera ? _screenService.MainCamera : _screenService.UICamera;
            enabled = false;
        }
    }
}