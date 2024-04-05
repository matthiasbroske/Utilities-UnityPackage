using UnityEngine;

namespace Matthias.Utilities
{
    public class Volume
    {
        /// <summary>
        /// Center position of first voxel.
        /// </summary>
        public Vector3 StartPosition { get; private set; }
        /// <summary>
        /// Spacing between voxels.
        /// </summary>
        public Vector3 VoxelSpacing { get; private set; }
        /// <summary>
        /// Number of voxels in each axis.
        /// </summary>
        public Vector3Int Dimensions { get; private set; }
        /// <summary>
        /// Voxels array.
        /// </summary>
        public float[] Voxels { get; private set; }
        /// <summary>
        /// Bounds of the volume.
        /// </summary>
        /// <value></value>
        public Bounds Bounds { get; private set; }

        /// <summary>
        /// Construct a volume.
        /// </summary>
        /// <param name="startPosition">Center position of volume's first voxel.</param>
        /// <param name="voxelSpacing">Spacing between voxels.</param>
        /// <param name="dimensions">Number of voxels along each axis.</param>
        /// <param name="voxels">Array of voxels.</param>
        public Volume(Vector3 startPosition, Vector3 voxelSpacing, Vector3Int dimensions, float[] voxels) {
            StartPosition = startPosition;
            VoxelSpacing = voxelSpacing;
            Dimensions = dimensions;
            Voxels = voxels;
            // Compute bounds given start position, dimensions, and voxel spacing
            Vector3 size = Vector3.Scale(voxelSpacing, dimensions);
            Vector3 center = startPosition - voxelSpacing/2 + size/2;  // Bounds start half-voxel outside of start position
            Bounds = new Bounds(center, size);
        }
    }
}
