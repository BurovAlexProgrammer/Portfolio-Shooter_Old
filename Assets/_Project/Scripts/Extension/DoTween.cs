using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Extension
{
    public partial class Common
    {
        public static async UniTask ToUniTaskWithCancel(this Tween tween, TweenCancelBehaviour cancelBehaviour, CancellationToken token)
        {
            await using var _ = token.Register(() =>
            {
                if (tween.IsActive())
                {
                    tween.Kill();
                }
            }, true);

            await tween.ToUniTask(cancelBehaviour, token);
        }
    }

    public static class TweenTemplates
    {
        public static Tween DOCustomShowWindow(this Transform transform)
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(transform.DOScale(0.01f, 0f))
                .Append(transform.DOScaleY(1f, 0.3f).SetEase(Ease.OutCubic))
                .Append(transform.DOScaleX(1f, 0.3f).SetEase(Ease.OutCubic))
                .SetUpdate(true);
            return sequence;
        }

        public static Tween DOCustomHideWindow(this Transform transform)
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(transform.DOScaleY(0.01f, 0.3f).SetEase(Ease.OutQuad))
                .Append(transform.DOScaleX(0f, 0.3f).SetEase(Ease.OutQuad))
                .Append(transform.DOScale(0f, 0f))
                .SetUpdate(true);
            return sequence;
        }
    }
}