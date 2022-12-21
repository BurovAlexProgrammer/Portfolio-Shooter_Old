using System;
using System.Linq;
using _Project.Scripts.Main.Localizations;
using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace _Project.Scripts.Main.Menu
{
    public class MenuSettingsView : MonoBehaviour
    {
        [SerializeField] private MenuSettingsController _controller;
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private VideoSettingViews _videoSettingViews;
        [SerializeField] private GameSettingViews _gameSettingViews;

        [Inject] private LocalizationService _localizationService;

        private void Start()
        {
            Init();
        }
        
        private void Awake()
        {
            _buttonApply.onClick.AddListener(_controller.Apply);
            _buttonReset.onClick.AddListener(ResetToDefault);
            _videoSettingViews.AntiAliasing.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessAntiAliasing));
            
            _videoSettingViews.Bloom.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessBloom));
            
            _videoSettingViews.Vignette.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessVignette));
            
            _videoSettingViews.AmbientOcclusion.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessAmbientOcclusion));
            
            _videoSettingViews.DepthOfField.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessDepthOfField));

            _gameSettingViews.CurrentLanguage.onValueChanged.AddListener(value =>
            {
                _controller.GameSettings.CurrentLocale = (Locales)value;
            });

            LoadLocalizationOptions();
        }
        
        private void OnDestroy()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonReset.onClick.RemoveAllListeners();
            _videoSettingViews.AntiAliasing.onValueChanged.RemoveAllListeners();
            _videoSettingViews.Bloom.onValueChanged.RemoveAllListeners();
            _videoSettingViews.Vignette.onValueChanged.RemoveAllListeners();
            _videoSettingViews.AmbientOcclusion.onValueChanged.RemoveAllListeners();
            _videoSettingViews.DepthOfField.onValueChanged.RemoveAllListeners();
            _gameSettingViews.CurrentLanguage.onValueChanged.RemoveAllListeners();
        }
        
        private async UniTask LoadLocalizationOptions()
        {
            var localizations = await _localizationService.GetLocalizationsAsync();
            _gameSettingViews.CurrentLanguage.options = localizations.Values.Select(x => new TMP_Dropdown.OptionData(x.Info.fullName)).ToList();
        }



        private void Init()
        {
            _videoSettingViews.AntiAliasing.isOn = _controller.VideoSettings.PostProcessAntiAliasing;
            _videoSettingViews.Bloom.isOn = _controller.VideoSettings.PostProcessBloom;
            _videoSettingViews.Vignette.isOn = _controller.VideoSettings.PostProcessVignette;
            _videoSettingViews.AmbientOcclusion.isOn = _controller.VideoSettings.PostProcessAmbientOcclusion;
            _videoSettingViews.DepthOfField.isOn = _controller.VideoSettings.PostProcessDepthOfField;
            _gameSettingViews.CurrentLanguage.value = (int)_controller.GameSettings.CurrentLocale;
        }

        private void ResetToDefault()
        {
            _controller.ResetToDefault();
            Init();
        }
        
        [Serializable]
        private class VideoSettingViews
        {
            public Toggle AntiAliasing;
            public Toggle Bloom;
            public Toggle Vignette;
            public Toggle AmbientOcclusion;
            public Toggle DepthOfField;
        }
        
        [Serializable]
        private class GameSettingViews
        {
            public TMP_Dropdown CurrentLanguage;
        }
    }
}
