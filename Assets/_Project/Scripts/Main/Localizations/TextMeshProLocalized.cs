using TMPro;
using UnityEngine;

namespace Main.Localizations
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProLocalized : LocalizedTextComponent
    {
        [SerializeField] private string _localizedTextKey;

        private TextMeshPro _textMesh;

        protected override void Awake()
        {
            base.Awake();
            _textMesh = GetComponent<TextMeshPro>();
        }

        protected override void SetText()
        {
            if (string.IsNullOrEmpty(_localizedTextKey))
            {
                _textMesh.text = "---NO KEY---";
                return;
            }
            _textMesh.text = _localization.GetLocalizedText(_localizedTextKey);
        }
    }
}