using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public static class Context
    {
        private static DiContainer _diContainer;

        public static DiContainer DiContainer
        {
            get => _diContainer;
            set => _diContainer ??= value;
        }
    }
}