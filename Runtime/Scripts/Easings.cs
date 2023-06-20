using UnityEngine;

namespace Matthias.Utilities
{
    public class Easings
    {
        public static float EaseInQuad(float t) => t * t;
        public static float EaseOutQuad(float t) => 1 - (1 - t) * (1 - t);
        public static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
}
