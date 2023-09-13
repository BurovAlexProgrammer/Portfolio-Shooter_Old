using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main
{
    public class CameraHolder : MonoBehaviour
    {
        private ScreenService _screenService;

        private void Awake()
        {
            _screenService = Context.GetService<ScreenService>();
        }

        private void OnDestroy()
        {
            if (_screenService == null || _screenService.CameraMain == null) return;
            ReturnCameraToService();
        }

        public void SetCamera()
        {
            Debug.Log("Camera was moved to cameraHolder (Click to select CameraHolder)", this);
            var mainCameraTransform = _screenService.CameraMain.transform;
            mainCameraTransform.parent = transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }

        public void ReturnCameraToService()
        {
            var mainCameraTransform = _screenService.CameraMain.transform;
            Debug.Log("Camera was moved to ScreenService", _screenService);
            mainCameraTransform.parent = _screenService.transform;
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localRotation = Quaternion.identity;
        }
    }
}