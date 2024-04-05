#if ENABLE_INPUT_SYSTEM
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Matthias.Utilities
{
    /// <summary>
    /// Scriptable object responsible for tracking the last input device used.
    /// </summary>
    /// <remarks>
    /// https://forum.unity.com/threads/detect-most-recent-input-device-type.753206/
    /// </remarks>
    public class InputDeviceHandler : MonoBehaviour
    {
        public static InputDevice LastDeviceUsed;
        public static event Action LastDeviceUsedChanged;
        
        public void Awake()
        {
            InputSystem.onEvent += OnInputSystemEvent;
            InputSystem.onDeviceChange += OnInputSystemDeviceChange;
        }
        
        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (LastDeviceUsed == device)
                return;
 
            // Some devices like to spam events like crazy.
            // Example: PS4 controller on PC keeps triggering events without meaningful change.
            var eventType = eventPtr.type;
            if (eventType == StateEvent.Type) {
                // Go through the changed controls in the event and look for ones actuated
                // above a magnitude of a little above zero.
                if (!eventPtr.EnumerateChangedControls(device: device, magnitudeThreshold: 0.0001f).Any())
                    return;
            }
 
 
            LastDeviceUsed = device;
            LastDeviceUsedChanged?.Invoke();
        }
        
        private void OnInputSystemDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (LastDeviceUsed == device)
                return;
 
            LastDeviceUsed = device;
            LastDeviceUsedChanged?.Invoke();
        }

        public void OnDestroy()
        {
            InputSystem.onEvent -= OnInputSystemEvent;
            InputSystem.onDeviceChange -= OnInputSystemDeviceChange;
        }
    }
}
#endif
