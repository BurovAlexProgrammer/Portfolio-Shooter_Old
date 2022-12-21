using _Project.Scripts.Extension;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Toggle
{
    [RequireComponent(typeof(UnityEngine.UI.Toggle))]
    public class ToggleView : MonoBehaviour
    {
        [SerializeField] private Image _handleImage;
        [SerializeField] private Image _handleBack;
        [SerializeField] private Color _activeHandleColor;
        [SerializeField] private Color _activeBackColor;

        private UnityEngine.UI.Toggle _toggle;
        private RectTransform _handleRect;
        private Vector3 _handleInactivePosition;
        private Vector3 _handleActivePosition;
        private Color _inactiveHandleColor;
        private Color _inactiveBackColor;

        private void Awake()
        {
            _toggle = GetComponent<UnityEngine.UI.Toggle>();
            _handleRect = _handleImage.GetComponent<RectTransform>();
            _handleInactivePosition = _handleRect.anchoredPosition;
            _handleActivePosition.Set(x: -_handleRect.rect.width / 2);
            _inactiveHandleColor = _handleImage.color;
            _inactiveBackColor = _handleBack.color;
            _toggle.onValueChanged.AddListener(OnSwitch);
        }

        private void Start()
        {
            OnSwitch(_toggle.isOn);
        }

        private void OnSwitch(bool newState)
        {
            var handlePosition = newState ? _handleActivePosition : _handleInactivePosition;
            var handleColor = newState ? _activeBackColor : _inactiveBackColor;
            var backColor = newState ? _activeHandleColor : _inactiveHandleColor; 
            _handleRect.DOAnchorPos(handlePosition, 0.4f).SetEase(Ease.InOutBack);
            _handleBack.DOColor(handleColor, 0.6f);
            _handleImage.DOColor(backColor, 0.4f);
        }
    }
}