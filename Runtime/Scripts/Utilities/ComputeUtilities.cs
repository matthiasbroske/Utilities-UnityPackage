using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias.Utilities
{
    public static class ComputeUtilities
    {
        public static Vector3Int GetThreadGroups(ComputeShader computeShader, int kernel, Vector2Int dimensions)
        {
            return GetThreadGroups(computeShader, kernel, new Vector3Int(dimensions.x, dimensions.y, 1));
        }
        
        public static Vector3Int GetThreadGroups(ComputeShader computeShader, int kernel, Vector3Int dimensions)
        {
            computeShader.GetKernelThreadGroupSizes(kernel, out uint threadGroupSizeX, out uint threadGroupSizeY, out uint threadGroupSizeZ);
            return new Vector3Int(Mathf.CeilToInt((float)dimensions.x / threadGroupSizeX), Mathf.CeilToInt((float)dimensions.y / threadGroupSizeY), Mathf.CeilToInt((float)dimensions.z / threadGroupSizeZ));
        }
        
        public static Vector3Int GetThreadGroups(uint threadGroupSizeX, uint threadGroupSizeY, uint threadGroupSizeZ, Vector3Int dimensions)
        {
            return new Vector3Int(Mathf.CeilToInt((float)dimensions.x / threadGroupSizeX), Mathf.CeilToInt((float)dimensions.y / threadGroupSizeY), Mathf.CeilToInt((float)dimensions.z / threadGroupSizeZ));
        }
    }
}
