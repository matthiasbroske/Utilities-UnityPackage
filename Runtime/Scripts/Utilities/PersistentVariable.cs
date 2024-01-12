using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Matthias.Utilities
{
    public interface IPersistentVariable<T>
    {
        public T Value { get; set; }
        public void ResetToDefault();
    }
    
    public class PersistentVariable<T> : IPersistentVariable<T>
    {
        private T _defaultValue;
        private string _uniqueKey;
        
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                switch (_value)
                {
                    case int valueAsInt:
                        PlayerPrefs.SetInt(_uniqueKey, valueAsInt);
                        break;
                    case float valueAsFloat:
                        PlayerPrefs.SetFloat(_uniqueKey, valueAsFloat);
                        break;
                    case string valueAsString:
                        PlayerPrefs.SetString(_uniqueKey, valueAsString);
                        break;
                    default:
                        PlayerPrefs.SetString(_uniqueKey, JsonConvert.SerializeObject(_value));
                        break;
                }
                PlayerPrefs.Save();
            }
        }

        public PersistentVariable(T defaultValue, string uniqueKey)
        {
            _defaultValue = defaultValue;
            _uniqueKey = uniqueKey;
            switch (defaultValue)
            {
                case int defaultValueAsInt:
                    _value = (T)Convert.ChangeType(PlayerPrefs.GetInt(_uniqueKey, defaultValueAsInt), typeof(T));
                    break;
                case float defaultValueAsFloat:
                    _value = (T)Convert.ChangeType(PlayerPrefs.GetFloat(_uniqueKey, defaultValueAsFloat), typeof(T));
                    break;
                case string defaultValueAsString:
                    _value = (T)Convert.ChangeType(PlayerPrefs.GetString(_uniqueKey, defaultValueAsString), typeof(T));
                    break;
                default:
                    _value = JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(_uniqueKey, JsonConvert.SerializeObject(defaultValue)));
                    break;
            }
        }

        public void ResetToDefault()
        {
            Value = _defaultValue;
        }
    }
}
