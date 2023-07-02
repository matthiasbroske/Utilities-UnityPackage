using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Matthias.Utilities
{
    /// <summary>
    /// Utility methods for dealing with input icons.
    /// </summary>
    [CreateAssetMenu(fileName = "InputBindingManager", menuName = "Utilities/Input/Binding Manager")]
    public class InputBindingManager : ScriptableObject
    {
        public struct Binding
        {
            public string displayString;
            public Sprite icon;
        }
        
        [Header("Input Icon Maps")] 
        [SerializeField] private InputIconMap _keyboardMouseIconMap;
        [SerializeField] private InputIconMap _dualShockGamepadIconMap;
        [SerializeField] private InputIconMap _xboxGamepadIconMap;

        private readonly string GamepadGroup = "Gamepad";
        private readonly string KeyboardMouseGroup = "KeyboardMouse";

        /// <summary>
        /// Get binding (name and icon) for given input action.
        /// </summary>
        public Binding GetBindingForAction(InputAction action)
        {
            Binding binding;

            // Get the current input device
            InputDevice inputDevice = InputDeviceHandler.LastDeviceUsed;
            // Get the binding index for that device on this action
            int bindingIndex = 0;
            if (TryGetGroupFromDevice(inputDevice, out string group))
            { 
                bindingIndex = action.GetBindingIndex(group);
                if (bindingIndex < 0)
                {
                    bindingIndex = 0;
                    Debug.LogErrorFormat("Ensure Input Actions asset has groups named {1} and {2}", inputDevice.name, 
                        GamepadGroup, KeyboardMouseGroup);
                }
            }
            else
            {
                Debug.LogErrorFormat("Unable to obtain group for device {0}. ", inputDevice.name);
            }
            // Get the display string and control path 
            binding.displayString = action.GetBindingDisplayString(bindingIndex, out _, out string controlPath);
            // Get the icon
            binding.icon = GetIconForBinding(inputDevice, controlPath);
            
            return binding;
        }
        
        // /// <summary>
        // /// Trys to get an icon for a binding specified by device layout and control path.
        // /// </summary>
        // public bool TryGetIconForBinding(string deviceLayoutName, string controlPath, out Sprite icon)
        // {
        //     icon = GetIconForBinding(deviceLayoutName, controlPath);
        //     return icon != null;
        // }
    
        /// <summary>
        /// Gets an icon for specified device control path.
        /// </summary>
        /// <param name="inputDevice"></param>
        /// <param name="controlPath"></param>
        /// <returns></returns>
        private Sprite GetIconForBinding(InputDevice inputDevice, string controlPath)
        {
            Sprite icon = null;
            if (InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "DualShockGamepad"))
                icon = _dualShockGamepadIconMap.GetIcon(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "Gamepad"))
                icon = _xboxGamepadIconMap.GetIcon(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "Mouse") ||
                     InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "Keyboard"))
                icon = _keyboardMouseIconMap.GetIcon(controlPath);
            return icon;
        }

        /// <summary>
        /// Very janky. Tries to get group for device by assuming input actions asset is already setup
        /// with groups named "Gamepad" and "KeyboardMouse".
        /// </summary>
        private bool TryGetGroupFromDevice(InputDevice inputDevice, out string group)
        {
            if (InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "Gamepad"))
            {
                group = GamepadGroup;
                return true;
            }

            if (InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "Keyboard") ||
                InputSystem.IsFirstLayoutBasedOnSecond(inputDevice.layout, "Mouse"))
            {
                group = KeyboardMouseGroup;
                return true;
            }

            group = null;
            return false;
        }
    }
}
