using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game
{
    public interface IGameState
    {
        public UniTask EnterState();
        public UniTask ExitState();
        public UniTask Update();

        public bool EqualsState(Type type)
        {
            return GetType() == type;
        }
    }
}