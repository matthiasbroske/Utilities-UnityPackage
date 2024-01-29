using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentFloat", menuName = "Utilities/Persistent Reference/Float")]
    public class PersistentFloatReference : PersistentVariableReference<float>
    {
        protected override float GetSavedValue()
        {
            return PlayerPrefs.GetFloat(_uniqueKey, _defaultValue);
        }

        protected override void SetSavedValue(float value)
        {
            PlayerPrefs.SetFloat(_uniqueKey, value);
        }
    }
}
