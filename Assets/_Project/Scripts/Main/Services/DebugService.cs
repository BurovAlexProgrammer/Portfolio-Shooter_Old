﻿using _Project.Scripts.Main.Game;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class DebugService : BaseService
    {
        [SerializeField] private DebugServiceConfig _serviceConfig;
        [SerializeField] private GizmoItem _explosionGizmoPrefab;
        [SerializeField] private Transform _gizmosContainer;

        public void CreateExplosionGizmo(Transform targetTransform, float radius)
        {
            if (_serviceConfig.ShowExplosionSphere == false) return;
            
            var gizmoInstance = Instantiate(_explosionGizmoPrefab, _gizmosContainer);
            gizmoInstance._transform.position = targetTransform.position;
            gizmoInstance._transform.DOScale(Vector3.one * radius * 2f, 0.2f).From(0f);
        }
    }
}