using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public abstract class GameState
    {
        public virtual async UniTask EnterState()
        {
        }

        public virtual async UniTask ExitState()
        {
        }

        public virtual async UniTask Update()
        {
        }

        public bool EqualsState(Type type)
        {
            return GetType() == type;
        }
    }
}