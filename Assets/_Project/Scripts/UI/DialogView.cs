using System;
using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private Button _buttonOk;
        [SerializeField] private Button _buttonCancel;
        [SerializeField] private Image _background;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Action<bool> Submit;

        private RectTransform _rectTransform;

        private const float _fadeDuration = 0.3f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _buttonOk.onClick.AddListener(() => Submit?.Invoke(true));
            _buttonCancel.onClick.AddListener(() => Submit?.Invoke(false));
        }

        private void OnDestroy()
        {
            _buttonOk.onClick.RemoveAllListeners();
            _buttonCancel.onClick.RemoveAllListeners();
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);
            _canvasGroup
                .DOFade(1f, _fadeDuration)
                .From(0f)
                .SetUpdate(true)
                .SetEase(Ease.InOutQuad);
            await _fadeDuration.WaitInSeconds();
        }
        
        public async UniTask Hide()
        {
            _canvasGroup
                .DOFade(0f, _fadeDuration)
                .SetUpdate(true)
                .SetEase(Ease.InOutQuad);
            await  _fadeDuration.WaitInSeconds();
            gameObject.SetActive(false);
        }

        public void Disable()
        {
            _canvasGroup.interactable = false;
        }
        
        public void Enable()
        {
            _canvasGroup.interactable = true;
        }
    }
}