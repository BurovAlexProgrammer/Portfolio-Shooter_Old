using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    [CreateAssetMenu(menuName = "Custom/Debug Config")]
    public class DebugServiceConfig : ScriptableObject
    {
        public bool ShowExplosionSphere;
    }
}