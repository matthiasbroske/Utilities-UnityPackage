using System;
using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentBool", menuName = "Utilities/Persistent Reference/Bool")]
    public class PersistentBoolReference : ScriptableObject, IPersistentVariable<bool>
    {
        [SerializeField] private bool _defaultValue;
        [SerializeField] private string _uniqueKey;
        
        private bool _value;

        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                PlayerPrefs.SetInt(_uniqueKey, BoolToInt(_value));
                PlayerPrefs.Save();
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<bool> OnValueChanged;

        void OnEnable()
        {
            _value = IntToBool(PlayerPrefs.GetInt(_uniqueKey, BoolToInt(_defaultValue)));
        }

        public void ResetToDefault()
        {
            Value = _defaultValue;
        }

        private int BoolToInt(bool boolean) => boolean ? 1 : 0;
        private bool IntToBool(int integer) => integer == 1;
    }
}
