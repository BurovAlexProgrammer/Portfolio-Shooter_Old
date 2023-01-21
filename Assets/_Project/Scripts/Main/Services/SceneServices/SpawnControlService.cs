using _Project.Scripts.Main.Game;
using UnityEngine;

namespace _Project.Scripts.Main.Services.SceneServices
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