using System;
using Main.Game;
using DG.Tweening;
using Main.Services;
using UnityEngine;

namespace Main.Services
{
    public class DebugService : MonoBehaviour, IService, IConstructInstaller
    {
        [SerializeField] private DebugServiceConfig _serviceConfig;
        [SerializeField] private GizmoItem _explosionGizmoPrefab;
        [SerializeField] private Transform _gizmosContainer;

        public bool SaveLogToFile => _serviceConfig.SaveLogToFile;

        public void CreateExplosionGizmo(Transform targetTransform, float radius)
        {
            throw new Exception("Not implemented service");
            if (_serviceConfig.ShowExplosionSphere == false) return;
            
            var gizmoInstance = Instantiate(_explosionGizmoPrefab, _gizmosContainer);
            gizmoInstance.transform.position = targetTransform.position;
            gizmoInstance.transform.DOScale(Vector3.one * radius * 2f, 0.2f).From(0f);
        }
        
        public void Construct(IServiceInstaller installer)
        {
            var serviceInstaller = installer as DebugServiceInstaller;
            _serviceConfig = serviceInstaller.Config;
            // _explosionGizmoPrefab = serviceInstaller.;
            // _gizmosContainer = serviceInstaller.;
        }
    }
}