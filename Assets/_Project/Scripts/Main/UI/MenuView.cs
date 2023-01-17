using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private const float _fadeDuration = 0.3f;

        public async UniTask Show()
        {
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, _fadeDuration).SetEase(Ease.InOutQuad);
            await _fadeDuration.WaitInSeconds();
        }
        
        public async UniTask Hide()
        {
            _canvasGroup.DOFade(0f, _fadeDuration).SetEase(Ease.InOutQuad);
            await _fadeDuration.WaitInSeconds();
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