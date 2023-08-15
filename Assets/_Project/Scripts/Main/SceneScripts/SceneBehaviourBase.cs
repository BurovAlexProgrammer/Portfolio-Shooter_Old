using _Project.Scripts.Main.Game;
using Main.Contexts;
using UnityEngine;

namespace _Project.Scripts.Main.SceneScripts
{
    public abstract class SceneBehaviourBase : MonoBehaviour, ISceneBehaviour
    {
        [SerializeField] private Transform _playerStartPoint;
        private PlayerBase _player;

        public void Start()
        {
            Debug.Log("Start");
            _player = Context.GetSceneObject<Player>();

            if (_player != null)
            {
                _player.CameraHolder.SetCamera();
                _player.Enable();
                _player.transform.position = _playerStartPoint.position;
                _player.transform.rotation = _playerStartPoint.rotation;
            }
        }
    }
}