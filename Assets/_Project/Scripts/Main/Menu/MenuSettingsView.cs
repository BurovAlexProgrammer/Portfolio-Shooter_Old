using System;
using System.Linq;
using _Project.Scripts.Main.Localizations;
using _Project.Scripts.Main.Services;
using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Button = UnityEngine.UI.Button;

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

        [Inject] private LocalizationService _localizationService;

        private void Start()
        {
            Init();
        }
        
        private void Awake()
        {
            _buttonSave.onClick.AddListener(SaveSettings);
            _buttonReset.onClick.AddListener(ResetToDefault);
            _videoSettingViews.AntiAliasing.onValueChanged.AddListener(
                value => _settingsController.Bind(value, ref _settingsController.VideoSettings.PostProcessAntiAliasing));
            
            _videoSettingViews.Bloom.onValueChanged.AddListener(
                value => _settingsController.Bind(value, ref _settingsController.VideoSettings.PostProcessBloom));
            
            _videoSettingViews.Vignette.onValueChanged.AddListener(
                value => _settingsController.Bind(value, ref _settingsController.VideoSettings.PostProcessVignette));
            
            _videoSettingViews.AmbientOcclusion.onValueChanged.AddListener(
                value => _settingsController.Bind(value, ref _settingsController.VideoSettings.PostProcessAmbientOcclusion));
            
            _videoSettingViews.DepthOfField.onValueChanged.AddListener(
                value => _settingsController.Bind(value, ref _settingsController.VideoSettings.PostProcessDepthOfField));

            _gameSettingViews.CurrentLanguage.onValueChanged.AddListener(value =>
            {
                _textRestartRequire.gameObject.SetActive(true);
                _settingsController.GameSettings.CurrentLocale = (Locales)value;
            });

            _ = LoadLocalizationOptions();
        }
        
        private void OnDestroy()
        {
            _buttonSave.onClick.RemoveAllListeners();
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
            _videoSettingViews.AntiAliasing.isOn = _settingsController.VideoSettings.PostProcessAntiAliasing;
            _videoSettingViews.Bloom.isOn = _settingsController.VideoSettings.PostProcessBloom;
            _videoSettingViews.Vignette.isOn = _settingsController.VideoSettings.PostProcessVignette;
            _videoSettingViews.AmbientOcclusion.isOn = _settingsController.VideoSettings.PostProcessAmbientOcclusion;
            _videoSettingViews.DepthOfField.isOn = _settingsController.VideoSettings.PostProcessDepthOfField;
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
