using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Game;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class DebugService : MonoBehaviour, IService
    {
        [SerializeField] private DebugServiceConfig _serviceConfig;
        [SerializeField] private GizmoItem _explosionGizmoPrefab;
        [SerializeField] private Transform _gizmosContainer;

        public bool SaveLogToFile => _serviceConfig.SaveLogToFile;

        public void CreateExplosionGizmo(Transform targetTransform, float radius)
        {
            if (_serviceConfig.ShowExplosionSphere == false) return;
            
            var gizmoInstance = Instantiate(_explosionGizmoPrefab, _gizmosContainer);
            gizmoInstance.transform.position = targetTransform.position;
            gizmoInstance.transform.DOScale(Vector3.one * radius * 2f, 0.2f).From(0f);
        }
    }
}