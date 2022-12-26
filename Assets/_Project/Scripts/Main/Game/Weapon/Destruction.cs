using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    public class Destruction : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 5f;
        
        public async UniTask DestroyOnLifetimeEnd()
        {
            await _lifeTime.WaitInSeconds();

            if (gameObject.IsDestroyed()) return;
            
            Destroy(gameObject);
        }
    }
}