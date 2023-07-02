using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias.Utilities
{
    /// <summary>
    /// Acts as an input icon map, who's primary ability
    /// is to return an icon for a given control path.
    /// </summary>
    [System.Serializable]
    public class InputIconMap
    {
        public static char IconNameSeparator = '-';
        public static char ControlPathSeparator = '/';
        
        [SerializeField] private Sprite[] _inputIcons;
        private Dictionary<string, int> _iconIndexByControlPath;

        public Sprite[] InputIcons => _inputIcons;
        
        /// <summary>
        /// Initialize a dictionary allowing for efficient icon lookup
        /// by control path. This relies on the fact that the sprite
        /// files are named accordingly to their control path.
        /// </summary>
        public void Init()
        {
            _iconIndexByControlPath = new Dictionary<string, int>();
            for (int i = 0; i < _inputIcons.Length; i++)
            {
                _iconIndexByControlPath[_inputIcons[i].name.Replace(IconNameSeparator, ControlPathSeparator)] = i;
            }
        }

        /// <summary>
        /// Returns the icon for the given control path.
        /// </summary>
        public Sprite GetIcon(string controlPath)
        {
            if (_iconIndexByControlPath == null) Init();
            
            if (_iconIndexByControlPath.TryGetValue(controlPath, out int value))
            {
                return _inputIcons[_iconIndexByControlPath[controlPath]];
            }
            else
            {
                return null;
            }
        }
    }
}
