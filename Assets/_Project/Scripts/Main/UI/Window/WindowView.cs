using System;
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

        public event Action<bool> DialogSwitched;
        public event Action Opened;
        public event Action Closed;
        
        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);
            await transform.DOCustomShowWindow().AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
            Opened?.Invoke();
        }

        public virtual async UniTask Close()
        {
            _canvasGroup.interactable = false;
            await transform.DOCustomHideWindow().AsyncWaitForCompletion();
            gameObject.SetActive(false);
            Closed?.Invoke();
        }

        protected virtual void OnDialogSwitched(bool state)
        {
            DialogSwitched?.Invoke(state);
        }
    }
}