#ifndef VOLUME_HELPERS_INCLUDED
#define VOLUME_HELPERS_INCLUDED

////////////////////////////////////////////////////////////////////////////////
///                                Uniforms                                  ///
////////////////////////////////////////////////////////////////////////////////
uint3 _Dimensions;  // Dimensions of the volume
float3 _VoxelStartPosition;  // Position of the first voxel in the volume
float3 _VoxelSpacing;  // Spacing between voxels (voxel size)

////////////////////////////////////////////////////////////////////////////////
///                                Methods                                   ///
////////////////////////////////////////////////////////////////////////////////

// Convert 3D voxel id to 1D voxel array index
uint VoxelIdx(uint x, uint y, uint z) {
    return x + y * _Dimensions.x + z * _Dimensions.x * _Dimensions.y;
}
uint VoxelIdx(uint3 voxelId) {
    return VoxelIdx(voxelId.x, voxelId.y, voxelId.z);
}

// Convert 1D voxel array index to 3D voxel id
uint3 VoxelIdxToId(uint idx)
{
    uint z = idx / (_Dimensions.x * _Dimensions.y);
    uint y = (idx - z * _Dimensions.x * _Dimensions.y) / _Dimensions.x;
    uint x = idx - y * _Dimensions.x - z * _Dimensions.x * _Dimensions.y;
    return uint3(x, y, z);
}

// Remap 3D voxel id to object space position
float3 RemapIDToPosition(uint3 voxelId)
{
    return _VoxelStartPosition + _VoxelSpacing * voxelId;
}
float3 RemapIDToPosition(float3 voxelId)
{
    return _VoxelStartPosition + _VoxelSpacing * voxelId;
}

// Remap uvw coords to object space position
float3 RemapUVWToPosition(float3 uvw)
{
    return _VoxelStartPosition + _VoxelSpacing * uvw * (_Dimensions - 1);
}

// Remap object space position to uvw coords
float3 RemapPositionToUVW(float3 position)
{
    return (position - _VoxelStartPosition) / (_VoxelSpacing * (_Dimensions - 1));
}

// Remap 3D voxel id to uvw coords
float3 RemapIDToUVW(uint3 i)
{
    return i / (_Dimensions-1.);
}

// Remap uvw coords to 3D voxel id
uint3 RemapUVWToID(float3 uvw)
{
    return floor(uvw * (_Dimensions-1));
}

#endif