using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.Menu
{
    public class MenuAboutDemoView : MenuView
    {
        [SerializeField] private RectTransform _layout;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _scrollDuration;

        private void Awake()
        {
            _buttonBack.onClick.AddListener(GoPrevMenu);
        }

        private void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }

        public override async UniTask Show()
        {
            await base.Show();

            await _layout
                .DOAnchorPosY(_endPositionY, _scrollDuration).SetEase(Ease.InOutSine)
                .AsyncWaitForCompletion();
        }
    }
}