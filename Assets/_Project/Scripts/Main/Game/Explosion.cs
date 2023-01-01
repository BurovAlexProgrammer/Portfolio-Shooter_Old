using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _radius = 2;
        [SerializeField] private float _force = 0.2f;
        [SerializeField] private float _liftForce = 0.1f;
        [SerializeField] private ForceMode _forceMode = ForceMode.Force;

        private async void Start()
        {
            DebugService.CreateExplosionGizmo(transform, _radius);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
            Debug.Log("Collider count: "+colliders.Length);
            foreach (var targetCollider in colliders)
            {
                var targetRigidbody = targetCollider.GetComponent<Rigidbody>();

                if (targetRigidbody != null)
                    targetRigidbody.AddExplosionForce(_force, transform.position, _radius, _liftForce, ForceMode.Impulse);
            }
        }
    }
}