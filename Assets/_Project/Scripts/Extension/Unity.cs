using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static void SetActive(this Scene scene, bool state)
        {
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                rootGameObject.SetActive(state);
            }
        }
        
        public static bool IsDestroyed(this GameObject gameObject)
        {
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }
    }
}