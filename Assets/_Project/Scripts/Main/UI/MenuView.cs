using System;
using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public event Action GoBack;
        
        // private const float FadeDuration = 0.3f;

        public virtual async UniTask Show()
        {
            // _canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            // await _canvasGroup
            //     .DOFade(1f, FadeDuration).SetEase(Ease.InOutQuad)
            //     .AsyncWaitForCompletion();

            await transform.DOCustomShowWindow().AsyncWaitForCompletion();
        }
        
        public virtual async UniTask Hide()
        {
            // await _canvasGroup
            //     .DOFade(0f, FadeDuration).SetEase(Ease.InOutQuad)
            //     .AsyncWaitForCompletion();

            await transform.DOCustomHideWindow().AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }

        public void HideImmediate()
        {
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

        public void GoPrevMenu()
        {
            GoBack?.Invoke();
        }
    }
}