using System;
using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

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
        GetComponent<Canvas>().worldCamera =
            _camera == CameraTypes.MainCamera ? _screenService.MainCamera : _screenService.UICamera;
        enabled = false;
    }
}
