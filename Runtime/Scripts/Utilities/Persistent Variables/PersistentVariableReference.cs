using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias.Utilities
{
    public abstract class PersistentVariableReference<T> : ScriptableObject, IPersistentVariable<T>
    {
        [SerializeField] protected T _defaultValue;
        [SerializeField] protected string _uniqueKey;
        
        protected T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                SetSavedValue(_value);
                PlayerPrefs.Save();
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> OnValueChanged;
        
        void OnEnable()
        {
            _value = GetSavedValue();
        }

        public void ResetToDefault()
        {
            Value = _defaultValue;
        }

        protected abstract T GetSavedValue();
        protected abstract void SetSavedValue(T value);
    }
}
