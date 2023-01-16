using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game
{
    public class GizmoItem : MonoBeh
    {
        public float ShowTime = 1f;

        private void Start()
        {
            _ = DestroyAfter(ShowTime);
        }

        private async UniTask DestroyAfter(float time)
        {
            await time.WaitInSeconds();
            
            Destroy(_gameObject);
        }
    }
}