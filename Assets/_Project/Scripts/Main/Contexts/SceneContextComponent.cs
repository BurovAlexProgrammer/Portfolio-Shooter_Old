using UnityEngine;

namespace Main.Contexts
{
    public class SceneContextComponent: MonoBehaviour 
    {
        private void Awake()
        {
            Context.InitScene();
        }
    }
}