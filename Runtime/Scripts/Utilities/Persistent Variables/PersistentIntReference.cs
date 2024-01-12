using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentInt", menuName = "Utilities/Persistent Reference/Int")]
    public class PersistentIntReference : ScriptableObject, IPersistentVariable<int>
    {
        [SerializeField] private int _defaultValue;
        [SerializeField] private string _uniqueKey;
        
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                PlayerPrefs.SetInt(_uniqueKey, _value);
                PlayerPrefs.Save();
            }
        }

        void OnEnable()
        {
            _value = PlayerPrefs.GetInt(_uniqueKey, _defaultValue);
        }

        public void ResetToDefault()
        {
            Value = _defaultValue;
        }
    }
}
