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
    }
}