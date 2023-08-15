using System;
using System.Linq;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Localizations;
using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.Menu
{
    public class MenuSettingsView : MenuView
    {
        [SerializeField] private MenuSettingsController _settingsController;
        [SerializeField] private Button _buttonSave;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private VideoSettingViews _videoSettingViews;
        [SerializeField] private GameSettingViews _gameSettingViews;
        [SerializeField] private TextMeshProUGUI _textRestartRequire;

        private LocalizationService _localizationService;

        private void Start()
        {
            Init();
        }
        
        private void Awake()
        {
            _localizationService = Context.GetService<LocalizationService>();
            _buttonSave.onClick.AddListener(SaveSettings);
            _buttonReset.onClick.AddListener(ResetToDefault);
            var videoSettings = _settingsController.VideoSettings;
            _videoSettingViews.AntiAliasingToggle.onValueChanged.AddListener(value => videoSettings.PostProcessAntiAliasing = value);
            _videoSettingViews.BloomToggle.onValueChanged.AddListener(value => videoSettings.PostProcessBloom = value);
            _videoSettingViews.VignetteToggle.onValueChanged.AddListener(value => videoSettings.PostProcessVignette = value);
            _videoSettingViews.AmbientOcclusionToggle.onValueChanged.AddListener(value => videoSettings.PostProcessAmbientOcclusion = value);
            _videoSettingViews.DepthOfFieldToggle.onValueChanged.AddListener(value => videoSettings.PostProcessDepthOfField = value);
            _videoSettingViews.FilmGrainToggle.onValueChanged.AddListener(value => videoSettings.PostProcessFilmGrain = value);
            _gameSettingViews.CurrentLanguage.onValueChanged.AddListener(value =>
            {
                _textRestartRequire.gameObject.SetActive(true);
                _settingsController.GameSettings.CurrentLocale = (Locales)value;
            });

            _ = LoadLocalizationOptions();
        }
        
        private void OnDestroy()
        {
            _buttonSave.onClick.RemoveListener(SaveSettings);
            _buttonReset.onClick.RemoveListener(ResetToDefault);
            _videoSettingViews.AntiAliasingToggle.onValueChanged.RemoveAllListeners();
            _videoSettingViews.BloomToggle.onValueChanged.RemoveAllListeners();
            _videoSettingViews.VignetteToggle.onValueChanged.RemoveAllListeners();
            _videoSettingViews.AmbientOcclusionToggle.onValueChanged.RemoveAllListeners();
            _videoSettingViews.DepthOfFieldToggle.onValueChanged.RemoveAllListeners();
            _videoSettingViews.FilmGrainToggle.onValueChanged.RemoveAllListeners();
            _gameSettingViews.CurrentLanguage.onValueChanged.RemoveAllListeners();
        }
        
        private async UniTask LoadLocalizationOptions()
        {
            var localizations = await _localizationService.GetLocalizationsAsync();
            _gameSettingViews.CurrentLanguage.options = localizations.Values.Select(x => new TMP_Dropdown.OptionData(x.Info.FullName)).ToList();
        }

        private void Init()
        {
            _videoSettingViews.AntiAliasingToggle.isOn = _settingsController.VideoSettings.PostProcessAntiAliasing;
            _videoSettingViews.BloomToggle.isOn = _settingsController.VideoSettings.PostProcessBloom;
            _videoSettingViews.VignetteToggle.isOn = _settingsController.VideoSettings.PostProcessVignette;
            _videoSettingViews.AmbientOcclusionToggle.isOn = _settingsController.VideoSettings.PostProcessAmbientOcclusion;
            _videoSettingViews.DepthOfFieldToggle.isOn = _settingsController.VideoSettings.PostProcessDepthOfField;
            _videoSettingViews.FilmGrainToggle.isOn = _settingsController.VideoSettings.PostProcessFilmGrain;
            _gameSettingViews.CurrentLanguage.value = (int)_settingsController.GameSettings.CurrentLocale;
        }

        private void SaveSettings()
        {
            _settingsController.Save();
            GoPrevMenu();
        }
        
        private void ResetToDefault()
        {
            _settingsController.ResetToDefault();
            Init();
        }
        
        [Serializable]
        private class VideoSettingViews
        {
            public Toggle AntiAliasingToggle;
            public Toggle BloomToggle;
            public Toggle VignetteToggle;
            public Toggle AmbientOcclusionToggle;
            public Toggle DepthOfFieldToggle;
            public Toggle FilmGrainToggle;
        }
        
        [Serializable]
        private class GameSettingViews
        {
            public TMP_Dropdown CurrentLanguage;
        }
    }
}
