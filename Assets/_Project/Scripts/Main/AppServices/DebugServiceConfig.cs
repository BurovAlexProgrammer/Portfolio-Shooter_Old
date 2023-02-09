using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    [CreateAssetMenu(menuName = "Custom/Debug Config")]
    public class DebugServiceConfig : ScriptableObject
    {
        public bool SaveLogToFile;
        public bool ShowExplosionSphere;
    }
}