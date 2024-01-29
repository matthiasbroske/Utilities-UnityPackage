using UnityEngine;

namespace Matthias.Utilities
{
    [CreateAssetMenu(fileName = "PersistentBool", menuName = "Utilities/Persistent Reference/Bool")]
    public class PersistentBoolReference : PersistentVariableReference<bool>
    {
        protected override bool GetSavedValue()
        {
            return IntToBool(PlayerPrefs.GetInt(_uniqueKey, BoolToInt(_defaultValue)));
        }

        protected override void SetSavedValue(bool value)
        {
            PlayerPrefs.SetInt(_uniqueKey, BoolToInt(value));
        }

        private int BoolToInt(bool boolean) => boolean ? 1 : 0;
        private bool IntToBool(int integer) => integer == 1;
    }
}
