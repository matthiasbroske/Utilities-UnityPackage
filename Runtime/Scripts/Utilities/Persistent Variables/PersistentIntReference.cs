using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentInt", menuName = "Utilities/Persistent Reference/Int")]
    public class PersistentIntReference : PersistentVariableReference<int>
    {
        protected override int GetSavedValue()
        {
            return PlayerPrefs.GetInt(_uniqueKey, _defaultValue);
        }

        protected override void SetSavedValue(int value)
        {
            PlayerPrefs.SetInt(_uniqueKey, value);
        }
    }
}
