using System.Threading;
using Main.Extension;
using Main.Wrappers;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Game.Player;
using Main.Services;
using UnityEngine;

namespace Main.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _startDelay = 3f;
        [SerializeField] private float _maxSpawnTime = 5f;
        [SerializeField] private AnimationCurve _difficultCurve;
        [SerializeField] private BasePoolItem _prefab;
        [Header("Info")]
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _timer;
        [SerializeField] private float _spawnTimer;
        [SerializeField] private float _timerMinutes;
        
        private PlayerBase _player;
        private IPoolService _poolService;
        
        private CancellationToken _cancellationToken;

        private bool _paused;

        private void Awake()
        {
            _player = Context.GetSceneObject<Player.Player>();
            _poolService = Context.GetService<PoolService>();
        }

        private void OnDisable()
        {
            StopSpawn();
        }

        private void OnDestroy()
        {
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

            while (!_cancellationToken.IsCancellationRequested && enabled)
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
            var instance = _poolService.Get(_prefab).GameObject;
            instance.transform.position = new Vector3 {x = Random.Range(-10f, 10f), z = Random.Range(-10f, 10f)};
            instance.gameObject.SetActive(true);
        }

    }
}