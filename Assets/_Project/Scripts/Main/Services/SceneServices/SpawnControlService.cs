using Main.Game;
using UnityEngine;

namespace Main.Services
{
    public class SpawnControlService : MonoBehaviour
    {
        [SerializeField] private bool _startOnEnable;
        [SerializeField] private Spawner[] _spawners;

        private bool _started;
        
        private void Start()
        {
            if (!_started && _startOnEnable)
            {
                _started = true;
                StartSpawn();
            }
        }
        
        private void OnEnable()
        {
            if (!_started) return;

            if (_startOnEnable)
            {
                StartSpawn();
            }
        }

        private void OnDisable()
        {
            StopSpawn();
        }

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