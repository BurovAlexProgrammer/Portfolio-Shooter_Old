using TMPro;
using UnityEngine;

namespace Main.UI
{
    public class TextField : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _valueText;
        
        public string Key => _key;
        public TextMeshProUGUI LabelText => _labelText;
        public TextMeshProUGUI ValueText => _valueText;
        
        
    }
}