using UnityEngine;

namespace Matthias.Utilities
{
    public class SDF : Volume
    {
        public SDF(Vector3 startPosition, Vector3 voxelSpacing, Vector3Int dimensions, float[] voxels) : 
            base(startPosition, voxelSpacing, dimensions, voxels){}
    }
}
