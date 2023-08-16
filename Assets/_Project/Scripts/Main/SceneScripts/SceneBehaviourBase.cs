using Main.Contexts;
using Main.Game.Player;
using UnityEngine;

namespace Main.SceneScripts
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