using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseShell : MonoBehaviour
    {
        [SerializeField] private ShellConfig _shellConfig;
        [SerializeField] private Destruction _destructionPrefab;
        [SerializeField] private float _lifeTime = 5f;

        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot(Transform startPoint)
        {
           gameObject.SetActive(true);
           transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
           _rigidbody.velocity = transform.forward * _shellConfig.InitSpeed;
        }

        public async UniTask DestroyOnLifetimeEnd()
        {
            await _lifeTime.WaitInSeconds();
                
            if (gameObject.IsDestroyed()) return;
            
            Destruct();
        }

        public void Destruct()
        {
            var destructTransform = Instantiate(_destructionPrefab).transform;
            destructTransform.SetPositionAndRotation(transform.position, transform.rotation);
            destructTransform.SetParent(transform.parent);
            Destroy(gameObject);
        }
    }
}