using UnityEngine;

namespace Matthias.Utilities
{
    public class RotateForever : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationAxis = Vector3.up;
        [SerializeField] private float _rotationSpeed = 45;

        void Update()
        {
            transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
        }
    }
}
