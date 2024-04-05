using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias.Utilities
{
    public static class RemapUtilities
    {
        public static float Remap(float dataValue, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (dataValue - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }
        
        public static float Remap(float dataValue, Vector2 fromRange, Vector2 toRange)
        {
            return Remap(dataValue, fromRange.x, fromRange.y, toRange.x, toRange.y);
        }
        
        public static float Remap(float dataValue, Vector4 range)
        {
            return Remap(dataValue, range.x, range.y, range.z, range.w);
        }
        
        public static float Remap01(float dataValue, float rangeMin, float rangeMax)
        {
            return Remap(dataValue, rangeMin, rangeMax, 0, 1);
        }
        
        public static float Remap01(float dataValue, Vector2 range)
        {
            return Remap01(dataValue, range.x, range.y);
        }

        public static Vector2 RemapVector01(Vector2 dataValue, Vector2 dataRangeMin, Vector2 dataRangeMax)
        {
            return new Vector2(
                Remap01(dataValue.x, new Vector2(dataRangeMin.x, dataRangeMax.x)),
                Remap01(dataValue.y, new Vector2(dataRangeMin.y, dataRangeMax.y))
            );
        }
        
        public static Vector3 RemapVector01(Vector3 dataValue, Vector3 dataRangeMin, Vector3 dataRangeMax)
        {
            return new Vector3(
                Remap01(dataValue.x, new Vector2(dataRangeMin.x, dataRangeMax.x)),
                Remap01(dataValue.y, new Vector2(dataRangeMin.y, dataRangeMax.y)),
                Remap01(dataValue.z, new Vector2(dataRangeMin.z, dataRangeMax.z))
            );
        }
    }
}
