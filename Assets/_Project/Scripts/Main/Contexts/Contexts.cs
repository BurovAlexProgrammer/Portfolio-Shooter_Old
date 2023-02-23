using System;
using _Project.Scripts.Main.Installers;

namespace _Project.Scripts.Main.Contexts
{
    public static class Contexts
    {
        private static IGamePlayContextInstaller _gamePlayContext;
        public static IGamePlayContextInstaller GamePlayContext
        {
            get => _gamePlayContext;
            set
            {
                if (_gamePlayContext != null)
                    throw new Exception("GamePlayContext already defined.");

                _gamePlayContext = value;
            }
        }
    }
}