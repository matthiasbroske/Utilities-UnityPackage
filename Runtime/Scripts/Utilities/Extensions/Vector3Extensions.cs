using UnityEngine;

namespace Matthias.Utilities
{
    public static class Vector2Extensions
    {
        public static Vector2 RotateBy(this Vector2 v, float angle, bool inDegrees = true)
        {
            float theta = inDegrees ? angle * Mathf.Deg2Rad : angle;
            return new Vector2(
                v.x * Mathf.Cos(theta) - v.y * Mathf.Sin(theta),
                v.x * Mathf.Sin(theta) + v.y * Mathf.Cos(theta)
            );
        }
    }
    
    public static class Vector3Extensions
    {
        public static Vector3 Floor(this Vector3 v)
        {
            return new Vector3(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
        }

        public static Vector3Int FloorToInt(this Vector3 v)
        {
            return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
        }

        public static Vector3 Ceil(this Vector3 v)
        {
            return new Vector3(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
        }

        public static Vector3Int CeilToInt(this Vector3 v)
        {
            return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
        }

        public static Vector3 Clamp01(this Vector3 v)
        {
            return new Vector3(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y), Mathf.Clamp01(v.z));
        }
    }
}
