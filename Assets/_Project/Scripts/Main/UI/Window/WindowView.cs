using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.UI.Window
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class WindowView : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;

        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);
            await transform.DOCustomShowWindow().AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
        }

        public virtual async UniTask Close()
        {
            _canvasGroup.interactable = false;
            await transform.DOCustomHideWindow().AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }
    }
}