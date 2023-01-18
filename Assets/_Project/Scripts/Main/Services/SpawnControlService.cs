using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Main.Services
{
    public class SpawnControlService : BaseService
    {
        [SerializeField] private Spawner[] _spawners;
        
        public void StartSpawn()
        {
            foreach (var spawner in _spawners)
            {
                spawner.StartSpawn();
            }
        }

        public void StopSpawn()
        {
            foreach (var spawner in _spawners)
            {
                spawner.StopSpawn();
            }
        }

        public void PauseSpawn()
        {
            foreach (var spawner in _spawners)
            {
                spawner.PauseSpawn();
            }
        }

        public void ContinueSpawn()
        {
            foreach (var spawner in _spawners)
            {
                spawner.ContinueSpawn();
            }
        }
    }
}