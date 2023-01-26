using _Project.Scripts.Extension;
using _Project.Scripts.Main.Menu;
using _Project.Scripts.Main.SceneScripts.MainMenu;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private MenuController _menuController;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private const float _fadeDuration = 0.3f;

        public MenuController MenuController => _menuController;

        public void Init(MenuController menuController)
        {
            _menuController = menuController;
        }
        
        public async UniTask Show()
        {
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            await _canvasGroup
                .DOFade(1f, _fadeDuration).SetEase(Ease.InOutQuad)
                .AsyncWaitForCompletion();
        }
        
        public async UniTask Hide()
        {
            await _canvasGroup
                .DOFade(0f, _fadeDuration).SetEase(Ease.InOutQuad)
                .AsyncWaitForCompletion();
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

        public void GoToPrevMenu()
        {
            _menuController.GoToPrevMenu();
        }
    }
}