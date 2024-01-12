using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentString", menuName = "Utilities/Persistent Reference/String")]
    public class PersistentStringReference : ScriptableObject, IPersistentVariable<string>
    {
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _uniqueKey;
        
        private string _value;

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                PlayerPrefs.SetString(_uniqueKey, _value);
                PlayerPrefs.Save();
            }
        }

        void OnEnable()
        {
            _value = PlayerPrefs.GetString(_uniqueKey, _defaultValue);
        }

        public void ResetToDefault()
        {
            Value = _defaultValue;
        }
    }
}
