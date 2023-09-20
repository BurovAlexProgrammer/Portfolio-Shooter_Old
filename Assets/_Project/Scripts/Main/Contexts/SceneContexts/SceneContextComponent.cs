using Main.Contexts.Installers;
using Main.DTOs;
using Main.Extension;
using UnityEngine;

namespace Main.Contexts
{
    public class SceneContextComponent : MonoBehaviour
    {
        [SerializeField] private SceneContextInstaller[] _installerPrefabs;

        private void Awake()
        {
            if (Context.ProjectContextInstaller == null)
            {
                var projectContextPrefab = Resources.Load<GameObject>(ResourceNames.ProjectContext);
                var instance = Instantiate(projectContextPrefab);
                instance.name = "Project Context";
            }

            if (_installerPrefabs != null)
            {
                for (var i = 0; i < _installerPrefabs.Length; i++)
                {
                    _installerPrefabs[i].Initialize();
                }
            }
        }
    }
}