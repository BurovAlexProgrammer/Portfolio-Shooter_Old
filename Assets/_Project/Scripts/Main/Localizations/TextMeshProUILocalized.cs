using TMPro;
using UnityEngine;

namespace Main.Localizations
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUILocalized : LocalizedTextComponent
    {
        [SerializeField] private string _localizedTextKey;
        [SerializeField] private string _prefix;
        [SerializeField] private string _postfix;

        private TextMeshProUGUI _textMesh;

        protected override void Awake()
        {
            base.Awake();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        protected override void SetText()
        {
            if (string.IsNullOrEmpty(_localizedTextKey))
            {
                _textMesh.text = "---NO KEY---";
                return;
            }
            _textMesh.text = _prefix + _localization.GetLocalizedText(_localizedTextKey) + _postfix;
        }
    }
}