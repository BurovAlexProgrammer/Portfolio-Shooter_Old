using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Main.Game
{
    public class Spawner : MonoBeh
    {
        [SerializeField] private float _startDelay = 3f;
        [SerializeField] private float _maxSpawnTime = 5f;
        [SerializeField] private AnimationCurve _difficultCurve;
        [SerializeField] private bool _startOnAwake;
        [SerializeField] private MonoPoolItemBase _prefab;
        [Header("Info")]
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _timer;
        [SerializeField] private float _spawnTimer;
        [SerializeField] private float _timerMinutes;
        
        [Inject] private PlayerBase _player;

        private bool _pause;

        private void Awake()
        {
            if (_startOnAwake) StartSpawn();
        }
        
        private void OnEnable() {}

        public void StartSpawn()
        {
            enabled = true;
            Spawning().Forget();
        }

        public void StopSpawn()
        {
            enabled = false;
        }

        public void PauseSpawn()
        {
            _pause = true;
        }

        public void ContinueSpawn()
        {
            _pause = false;
        }

        private async UniTask Spawning()
        {
            await _startDelay.WaitInSeconds();

            while (enabled)
            {
                await UniTask.NextFrame();
                
                if (_pause) continue;
                
                _timer += Time.deltaTime;
                _spawnTimer -= Time.deltaTime;
                
                if (_spawnTimer <= 0f)
                {
                    Spawn();
                    SetSpawnTimer();
                }
            }
        }

        private void SetSpawnTimer()
        {
            _timerMinutes = _timer / 60f;
            _spawnRate = _difficultCurve.Evaluate(_timerMinutes);
            _spawnTimer = 1f / _spawnRate;
            _spawnTimer = Mathf.Min(_spawnTimer, _maxSpawnTime);
        }

        private void Spawn()
        {
            var t = _player;
            var instance = Services.Services.PoolService.Get(_prefab);
            instance._transform.position = new Vector3 {x = Random.Range(-10f, 10f), z = Random.Range(-10f, 10f)};
            instance.gameObject.SetActive(true);
        }

    }
}