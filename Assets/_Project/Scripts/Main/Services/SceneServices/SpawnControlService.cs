using _Project.Scripts.Main.Game;
using UnityEngine;

namespace _Project.Scripts.Main.Services.SceneServices
{
    public class SpawnControlService : BaseService
    {
        [SerializeField] private Spawner[] _spawners;
        
        public void StartSpawn()
        {
            for (var i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].StartSpawn();
            }
        }

        public void StopSpawn()
        {
            for (var i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].StopSpawn();
            }
        }

        public void PauseSpawn()
        {
            for (var i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].PauseSpawn();
            }
        }

        public void ContinueSpawn()
        {
            for (var i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].ContinueSpawn();
            }
        }
    }
}