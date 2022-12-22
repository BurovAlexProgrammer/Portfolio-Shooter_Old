using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public abstract class BasePlayer : MonoBehaviour
    {
        [SerializeField] private CameraHolder _cameraHolder;

        public CameraHolder CameraHolder => _cameraHolder;
    }
}