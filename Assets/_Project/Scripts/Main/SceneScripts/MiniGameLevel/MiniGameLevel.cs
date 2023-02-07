using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.SceneScripts.MiniGameLevel
{
    public class MiniGameLevel : MonoBehaviour
    {
        [Inject] private PlayerBase _player;

        void Start()
        {
            _player.CameraHolder.SetCamera();
            _player.Enable();
        }
    }
}
