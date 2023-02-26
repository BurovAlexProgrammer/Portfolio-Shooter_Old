using _Project.Scripts.Main.Game.Health;
using NSubstitute;

namespace Tests.PlayMode.Health
{
    public static class Create
    {
        public static PlayerHealth HealthBase()
        {
            return Substitute.For<PlayerHealth>();
        }
    }
}