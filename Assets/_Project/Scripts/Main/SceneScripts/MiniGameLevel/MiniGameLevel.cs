using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

public class MiniGameLevel : MonoBehaviour
{
    [Inject] private PlayerBase _player;

    void Start()
    {
        _player.CameraHolder.SetCamera();
        _player.Enable();
    }
}
