using System;
using UnityEngine;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Toggle))]
    public class SimpleToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private ToggleActions _toggleAction;

        private UnityEngine.UI.Toggle _toggle;

        private enum ToggleActions
        {
            GameObjectActive
        }

        void Start()
        {
            _toggle = GetComponent<UnityEngine.UI.Toggle>();
            _toggle.onValueChanged.AddListener(ToggleAction);
            ToggleAction(_toggle.isOn);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }

        private void ToggleAction(bool newValue)
        {
            switch (_toggleAction)
            {
                case ToggleActions.GameObjectActive:
                    _target.gameObject.SetActive(newValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
