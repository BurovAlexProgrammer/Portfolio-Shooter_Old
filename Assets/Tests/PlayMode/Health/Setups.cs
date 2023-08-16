using Main.Game.Health;

namespace Tests.PlayMode.Health
{
    public static class Setups
    {
        public static void DefaultSetup(this PlayerHealth health)
        {
            health.Init(Constants.DefaultHealthCurrent, Constants.DefaultHealthMax);
        }

        public static void Setup(this PlayerHealth health, float currentHealth, float maxHealth)
        {
            health.Init(currentHealth, maxHealth);
        }
    }
}