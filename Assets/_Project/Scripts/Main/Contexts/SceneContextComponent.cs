using UnityEngine;

namespace Main.Contexts
{
    public class SceneContextComponent : MonoBehaviour
    {
        private void Awake()
        {
            if (Context.ProjectContextInstaller == null)
            {
                var projectContextPrefab = Resources.Load<GameObject>("ProjectContext");
                Instantiate(projectContextPrefab);
            }

            Context.InitScene();
        }
    }
}