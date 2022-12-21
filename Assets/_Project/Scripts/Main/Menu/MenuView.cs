using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuView : MonoWrapper
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private const float _hideDuration = 0.3f;

        public async UniTask Show()
        {
            _canvasGroup.alpha = 0f;
            _gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, _hideDuration).SetEase(Ease.InOutQuad);
            await UniTask.Delay(_hideDuration.ToMillisecs());
        }
        
        public async UniTask Hide()
        {
            _canvasGroup.DOFade(0f, _hideDuration).SetEase(Ease.InOutQuad);
            await UniTask.Delay(_hideDuration.ToMillisecs());
            _gameObject.SetActive(false);
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