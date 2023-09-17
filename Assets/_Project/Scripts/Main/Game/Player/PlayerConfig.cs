using Main.Extension.Attributes;
using UnityEngine;

namespace Main.Game
{
    [CreateAssetMenu(menuName = "Custom/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _shootSpeed;
        [SerializeField] private float _maxVerticalAngle;
        [SerializeField] [Range(0f, 1f)] private float _moveLerpTime;
        [SerializeField] [Range(0f, 1f)] private float _rotateLerpTime;
        [SerializeField] private float _shootDelay;

        public float MoveSpeed => _moveSpeed;
        public float RunSpeed => _runSpeed;
        public float RotateSpeed => _rotateSpeed;
        public float ShootSpeed => _shootSpeed;
        public float MaxVerticalAngle => _maxVerticalAngle;
        public float ShootDelay => _shootDelay;
        public float MoveLerpTime => _moveLerpTime;
        public float RotateLerpTime => _rotateLerpTime;
    }
}