using Main.Contexts.Installers;
using Main.Game;
using UnityEngine;

namespace _Project.Scripts.Main.Contexts.SceneContexts
{
    public class DefaultGameSceneContextInstaller : SceneContextInstaller
    {
        [SerializeField] private Player _player;
        
        protected override void InstallBindings()
        {
            // Context.
        }
    }
}