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
        [SerializeField] private MonoPoolItemBase _prefab;
        [Header("Info")]
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _timer;
        [SerializeField] private float _spawnTimer;
        [SerializeField] private float _timerMinutes;
        
        [Inject] private PlayerBase _player;

        private bool _paused;
        
        private void OnDisable()
        {
            StopSpawn();
        }

        public void StartSpawn()
        {
            enabled = true;
            _ = Spawning();
        }

        public void StopSpawn()
        {
            enabled = false;
        }

        public void PauseSpawn()
        {
            _paused = true;
        }

        public void ContinueSpawn()
        {
            _paused = false;
        }

        private async UniTask Spawning()
        {
            await _startDelay.WaitInSeconds();

            while (this != null && enabled)
            {
                if (_paused) continue;
                
                _timer += Time.deltaTime;
                _spawnTimer -= Time.deltaTime;
                
                if (_spawnTimer <= 0f)
                {
                    Spawn();
                    SetSpawnTimer();
                }
                
                await UniTask.NextFrame();
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
            var instance = AppServices.Services.PoolService.Get(_prefab);
            instance.Transform.position = new Vector3 {x = Random.Range(-10f, 10f), z = Random.Range(-10f, 10f)};
            instance.gameObject.SetActive(true);
        }

    }
}