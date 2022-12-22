using _Project.Scripts.Game;
using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public class SceneLocationInstaller : MonoInstaller
    {
        [SerializeField] private BasePlayer _playerPrefab;
        [SerializeField] private Transform _playerStartPoint;
        public override void InstallBindings()
        {
            var playerInstance = 
                Container.InstantiatePrefabForComponent<BasePlayer>(_playerPrefab, _playerStartPoint.position, _playerStartPoint.rotation, null);
            playerInstance.name = "Player";
            

            Container.Bind<BasePlayer>().FromInstance(playerInstance).AsSingle();
        }
    }
}
