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
                Debug.Log("I'm setting the value, shoops.");
                _value = value;
                PlayerPrefs.SetInt(_uniqueKey, BoolToInt(_value));
                PlayerPrefs.Save();
            }
        }

        void OnEnable()
        {
            Debug.Log("I keep enabling, hehe");
            _value = IntToBool(PlayerPrefs.GetInt(_uniqueKey, BoolToInt(_defaultValue)));
        }

        public void ResetToDefault()
        {
            Debug.Log("someone's calling reset!");
            Value = _defaultValue;
        }

        private int BoolToInt(bool boolean) => boolean ? 1 : 0;
        private bool IntToBool(int integer) => integer == 1;
    }
}
