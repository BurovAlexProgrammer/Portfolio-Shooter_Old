using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

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
}