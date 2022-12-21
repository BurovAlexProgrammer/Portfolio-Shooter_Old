using System;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Extension
{
    public enum BindActions
    {
        Started,
        Performed,
        Canceled
    }

    public static partial class Common
    {
        public static void BindAction(this InputAction inputAction, BindActions bindAction,
            Action<InputAction.CallbackContext> action)
        {
            switch (bindAction)
            {
                case BindActions.Performed:
                    inputAction.started += (ctx) => _ = BindPerformed(ctx, action);
                    break;
                case BindActions.Started:
                    inputAction.started += action;
                    break;
                case BindActions.Canceled:
                    inputAction.canceled += action;
                    break;
                default:
                    throw new Exception("Error while BindAction for InputSystem.");
            }
        }

        private static async UniTask BindPerformed(InputAction.CallbackContext ctx,
            Action<InputAction.CallbackContext> action)
        {
            while (ctx.action.inProgress)
            {
                action?.Invoke(ctx);
                await UniTask.NextFrame();
            }
        }
    }
}