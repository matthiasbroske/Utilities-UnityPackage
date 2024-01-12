using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Matthias.Utilities
{
    [RequireComponent(typeof(Toggle))]
    public class PersistentBoolToggleInitializer : MonoBehaviour
    {
        [SerializeField] private PersistentBoolReference _persistentBool;
        private Toggle _toggle;
        private UnityAction<bool> _onValueChangedHandler;
        
        void OnEnable()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.isOn = _persistentBool.Value;
            _onValueChangedHandler = (isOn) => _persistentBool.Value = isOn; 
            _toggle.onValueChanged.AddListener(_onValueChangedHandler);
        }

        void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(_onValueChangedHandler);
        }
    }
}
