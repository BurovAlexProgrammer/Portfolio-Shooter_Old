using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Extension;
using Main.Services;
using UnityEngine;

namespace Main.SceneScripts.Intro
{
    public class IntroSceneBehaviour: SceneBehaviourBase
    {
        [SerializeField] private float _introDuration = 2f;
        [SerializeField] private string _nextSceneName;
        

        protected override async void Awake()
        {
            base.Awake();
            await _introDuration.WaitInSeconds();
            Context.Resolve<SceneLoaderService>().LoadSceneAsync(_nextSceneName).Forget();
        }
    }
}