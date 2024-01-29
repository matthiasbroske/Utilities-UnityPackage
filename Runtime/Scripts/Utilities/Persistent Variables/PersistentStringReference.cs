using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentString", menuName = "Utilities/Persistent Reference/String")]
    public class PersistentStringReference : PersistentVariableReference<string>
    {
        protected override string GetSavedValue()
        {
            return PlayerPrefs.GetString(_uniqueKey, _defaultValue);
        }

        protected override void SetSavedValue(string value)
        {
            PlayerPrefs.SetString(_uniqueKey, value);
        }
    }
}
