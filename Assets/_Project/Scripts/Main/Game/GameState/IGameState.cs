﻿using System;
using Cysharp.Threading.Tasks;

namespace Main.Game.GameState
{
    public interface IGameState
    {
        public bool EqualsState(Type type) => GetType() == type;

        public virtual async UniTask EnterState()
        {
            await UniTask.Yield();
        }

        public virtual async UniTask ExitState()
        {
            await UniTask.Yield();
        }

        public virtual async UniTask Update()
        {
            await UniTask.Yield();
        }
    }
}