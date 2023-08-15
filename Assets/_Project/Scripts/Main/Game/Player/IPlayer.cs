using _Project.Scripts.Main.Game.Health;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public interface IPlayer
    {
        CameraHolder CameraHolder { get; }
        HealthBase Health { get; }
        void Disable();
        void Enable();
        void Move(Vector2 inputValue);
        void Rotate(Vector2 inputValue);
    }
}