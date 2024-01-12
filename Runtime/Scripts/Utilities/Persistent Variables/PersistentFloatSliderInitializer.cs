using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Matthias.Utilities
{
    [RequireComponent(typeof(Slider))]
    public class PersistentFloatSliderInitializer : MonoBehaviour
    {
        [SerializeField] private PersistentFloatReference _persistentFloat;
        private Slider _slider;
        private UnityAction<float> _onValueChangedHandler;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        
        void OnEnable()
        {
            _slider.value = _persistentFloat.Value;
            _onValueChangedHandler = (value) => _persistentFloat.Value = value; 
            _slider.onValueChanged.AddListener(_onValueChangedHandler);
            _persistentFloat.OnValueChanged += _slider.SetValueWithoutNotify;
        }

        void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(_onValueChangedHandler);
            _persistentFloat.OnValueChanged -= _slider.SetValueWithoutNotify;
        }
    }
}
