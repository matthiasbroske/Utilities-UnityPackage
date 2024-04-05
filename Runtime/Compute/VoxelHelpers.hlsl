#ifndef VOXEL_HELPERS_INCLUDED
#define VOXEL_HELPERS_INCLUDED

#include "VolumeHelpers.hlsl"
// _Dimensions (uint3);
// _VoxelStartPosition (float3);
// _VoxelSpacing (float3);

////////////////////////////////////////////////////////////////////////////////
///                                Uniforms                                  ///
////////////////////////////////////////////////////////////////////////////////
StructuredBuffer<float> _Voxels;
bool _FlipNormals;

////////////////////////////////////////////////////////////////////////////////
///                                Methods                                   ///
////////////////////////////////////////////////////////////////////////////////

// Sample voxel value at 3D voxel id
float VoxelValue(uint x, uint y, uint z)
{
    return _Voxels[VoxelIdx(x, y, z)];
}
float VoxelValue(uint3 voxelId)
{
    return VoxelValue(voxelId.x, voxelId.y, voxelId.z);
}

// Compute voxel gradient at 3D voxel id via central differences
float3 VoxelGradient(uint3 voxelId)
{
    uint3 i_n = max(voxelId, 1) - 1;
    uint3 i_p = min(voxelId + 1, _Dimensions - 1);
    float v_nx = VoxelValue(i_n.x, voxelId.y, voxelId.z);
    float v_px = VoxelValue(i_p.x, voxelId.y, voxelId.z);
    float v_ny = VoxelValue(voxelId.x, i_n.y, voxelId.z);
    float v_py = VoxelValue(voxelId.x, i_p.y, voxelId.z);
    float v_nz = VoxelValue(voxelId.x, voxelId.y, i_n.z);
    float v_pz = VoxelValue(voxelId.x, voxelId.y, i_p.z);
    return (_FlipNormals ? -1 : 1) * float3(
        (v_px - v_nx)/(2*_VoxelSpacing.x),
        (v_py - v_ny)/(2*_VoxelSpacing.y),
        (v_pz - v_nz)/(2*_VoxelSpacing.z)
    );
}

// Trilinearly sample voxel buffer 
float FloatValueTrilinear(float3 uvw, StructuredBuffer<float> buffer) {
    // Setup
    float3 pos = saturate(uvw) * (_Dimensions-1);  // We want position to go from (0,0,0) -> (_Dimensions.x-1,_Dimensions.y-1,_Dimensions.z-1)
    float3 t = pos-floor(pos);
    uint3 voxel0 = floor(pos);
    uint3 voxel1 = ceil(pos);

    // Get voxel at each corner
    float c000 = buffer[VoxelIdx(voxel0)];
    float c100 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.yz))];
    float c001 = buffer[VoxelIdx(uint3(voxel0.xy, voxel1.z))];
    float c101 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.y, voxel1.z))];
    float c010 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.y, voxel0.z))];
    float c110 = buffer[VoxelIdx(uint3(voxel1.xy, voxel0.z))];
    float c011 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.yz))];
    float c111 = buffer[VoxelIdx(voxel1)];
    // Lerp voxels along x
    float c00 = lerp(c000, c100, t.x);
    float c01 = lerp(c001, c101, t.x);
    float c10 = lerp(c010, c110, t.x);
    float c11 = lerp(c011, c111, t.x);
    // Lerp voxels along z
    float c0 = lerp(c00, c01, t.z);
    float c1 = lerp(c10, c11, t.z);
    // Lerp voxels along y
    float c = lerp(c0, c1, t.y);

    return c;
}
float FloatValueTrilinear(float3 uvw, RWStructuredBuffer<float> buffer) {
    // Setup
    float3 pos = saturate(uvw) * (_Dimensions-1);  // We want position to go from (0,0,0) -> (_Dimensions.x-1,_Dimensions.y-1,_Dimensions.z-1)
    float3 t = pos-floor(pos);
    uint3 voxel0 = floor(pos);
    uint3 voxel1 = ceil(pos);

    // Get voxel at each corner
    float c000 = buffer[VoxelIdx(voxel0)];
    float c100 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.yz))];
    float c001 = buffer[VoxelIdx(uint3(voxel0.xy, voxel1.z))];
    float c101 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.y, voxel1.z))];
    float c010 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.y, voxel0.z))];
    float c110 = buffer[VoxelIdx(uint3(voxel1.xy, voxel0.z))];
    float c011 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.yz))];
    float c111 = buffer[VoxelIdx(voxel1)];
    // Lerp voxels along x
    float c00 = lerp(c000, c100, t.x);
    float c01 = lerp(c001, c101, t.x);
    float c10 = lerp(c010, c110, t.x);
    float c11 = lerp(c011, c111, t.x);
    // Lerp voxels along z
    float c0 = lerp(c00, c01, t.z);
    float c1 = lerp(c10, c11, t.z);
    // Lerp voxels along y
    float c = lerp(c0, c1, t.y);

    return c;
}
float2 Float2ValueTrilinear(float3 uvw, RWStructuredBuffer<float2> buffer) {
    // Setup
    float3 pos = saturate(uvw) * (_Dimensions-1);  // We want position to go from (0,0,0) -> (_Dimensions.x-1,_Dimensions.y-1,_Dimensions.z-1)
    float3 t = pos-floor(pos);
    uint3 voxel0 = floor(pos);
    uint3 voxel1 = ceil(pos);

    // Get voxel at each corner
    float2 c000 = buffer[VoxelIdx(voxel0)];
    float2 c100 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.yz))];
    float2 c001 = buffer[VoxelIdx(uint3(voxel0.xy, voxel1.z))];
    float2 c101 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.y, voxel1.z))];
    float2 c010 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.y, voxel0.z))];
    float2 c110 = buffer[VoxelIdx(uint3(voxel1.xy, voxel0.z))];
    float2 c011 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.yz))];
    float2 c111 = buffer[VoxelIdx(voxel1)];
    // Lerp voxels along x
    float2 c00 = lerp(c000, c100, t.x);
    float2 c01 = lerp(c001, c101, t.x);
    float2 c10 = lerp(c010, c110, t.x);
    float2 c11 = lerp(c011, c111, t.x);
    // Lerp voxels along z
    float2 c0 = lerp(c00, c01, t.z);
    float2 c1 = lerp(c10, c11, t.z);
    // Lerp voxels along y
    float2 c = lerp(c0, c1, t.y);

    return c;
}
float3 Float3ValueTrilinear(float3 uvw, RWStructuredBuffer<float3> buffer) {
    // Setup
    float3 pos = saturate(uvw) * (_Dimensions-1);  // We want position to go from (0,0,0) -> (_Dimensions.x-1,_Dimensions.y-1,_Dimensions.z-1)
    float3 t = pos-floor(pos);
    uint3 voxel0 = floor(pos);
    uint3 voxel1 = ceil(pos);

    // Get voxel at each corner
    float3 c000 = buffer[VoxelIdx(voxel0)];
    float3 c100 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.yz))];
    float3 c001 = buffer[VoxelIdx(uint3(voxel0.xy, voxel1.z))];
    float3 c101 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.y, voxel1.z))];
    float3 c010 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.y, voxel0.z))];
    float3 c110 = buffer[VoxelIdx(uint3(voxel1.xy, voxel0.z))];
    float3 c011 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.yz))];
    float3 c111 = buffer[VoxelIdx(voxel1)];
    // Lerp voxels along x
    float3 c00 = lerp(c000, c100, t.x);
    float3 c01 = lerp(c001, c101, t.x);
    float3 c10 = lerp(c010, c110, t.x);
    float3 c11 = lerp(c011, c111, t.x);
    // Lerp voxels along z
    float3 c0 = lerp(c00, c01, t.z);
    float3 c1 = lerp(c10, c11, t.z);
    // Lerp voxels along y
    float3 c = lerp(c0, c1, t.y);

    return c;
}
float4 Float4ValueTrilinear(float3 uvw, RWStructuredBuffer<float4> buffer) {
    // Setup
    float3 pos = saturate(uvw) * (_Dimensions-1);  // We want position to go from (0,0,0) -> (_Dimensions.x-1,_Dimensions.y-1,_Dimensions.z-1)
    float3 t = pos-floor(pos);
    uint3 voxel0 = floor(pos);
    uint3 voxel1 = ceil(pos);

    // Get voxel at each corner
    float4 c000 = buffer[VoxelIdx(voxel0)];
    float4 c100 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.yz))];
    float4 c001 = buffer[VoxelIdx(uint3(voxel0.xy, voxel1.z))];
    float4 c101 = buffer[VoxelIdx(uint3(voxel1.x, voxel0.y, voxel1.z))];
    float4 c010 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.y, voxel0.z))];
    float4 c110 = buffer[VoxelIdx(uint3(voxel1.xy, voxel0.z))];
    float4 c011 = buffer[VoxelIdx(uint3(voxel0.x, voxel1.yz))];
    float4 c111 = buffer[VoxelIdx(voxel1)];
    // Lerp voxels along x
    float4 c00 = lerp(c000, c100, t.x);
    float4 c01 = lerp(c001, c101, t.x);
    float4 c10 = lerp(c010, c110, t.x);
    float4 c11 = lerp(c011, c111, t.x);
    // Lerp voxels along z
    float4 c0 = lerp(c00, c01, t.z);
    float4 c1 = lerp(c10, c11, t.z);
    // Lerp voxels along y
    float4 c = lerp(c0, c1, t.y);

    return c;
}

// Trilinearly sample _Voxels buffer
float VoxelValueTrilinear(float3 uvw) {
    return FloatValueTrilinear(uvw, _Voxels);
}

#endif