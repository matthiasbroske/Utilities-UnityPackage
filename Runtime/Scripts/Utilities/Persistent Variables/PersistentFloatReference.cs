using System;
using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentFloat", menuName = "Utilities/Persistent Reference/Float")]
    public class PersistentFloatReference : ScriptableObject, IPersistentVariable<float>
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private string _uniqueKey;
        
        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                PlayerPrefs.SetFloat(_uniqueKey, _value);
                PlayerPrefs.Save();
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<float> OnValueChanged;
        
        void OnEnable()
        {
            _value = PlayerPrefs.GetFloat(_uniqueKey, _defaultValue);
        }

        public void ResetToDefault()
        {
            Value = _defaultValue;
        }
    }
}
