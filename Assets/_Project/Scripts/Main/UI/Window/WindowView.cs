using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.UI.Window
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class WindowView : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;

        public async void Show()
        {
            gameObject.SetActive(true);
            await transform.DOScale(1f, 0.3f).From(0f)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
        }

        public async UniTask Close()
        {
            _canvasGroup.interactable = false;
            await transform.DOScale(0f, 0.3f)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }
    }
}