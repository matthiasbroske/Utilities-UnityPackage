#ifndef SHADER_UTILITIES_INCLUDED
#define SHADER_UTILITIES_INCLUDED

#define INFINITY 1e34
#define PI 3.1415926535

float Remap(float dataValue, float4 dataRange)
{
    return dataRange.z + (dataValue - dataRange.x) * (dataRange.w - dataRange.z) / (dataRange.y - dataRange.x);
}

float Remap01(float dataValue, float2 dataRange)
{
    return (dataValue - dataRange.x) / (dataRange.y - dataRange.x);
}

// Returns intersection distance and depth
float2 RaySphereIntersection(float3 rayOrigin, float3 rayDirection, float3 spherePosition, float sphereRadius)
{
    // Get the closest point along the ray to the center of the sphere
    float t = dot(spherePosition-rayOrigin, normalize(rayDirection));
    float3 closestPoint = rayOrigin + rayDirection*t;
    float closestDistance = length(spherePosition-closestPoint);
    // Return early if no intersection
    if (closestDistance >= sphereRadius)
        return float2(INFINITY, 0);
    // Otherwise return distance to intersection as well as intersection depth
    float x = sqrt(sphereRadius*sphereRadius - closestDistance*closestDistance);
    float t1 = max(t-x, 0.);  // Set distance to intersection to 0 when inside sphere
    float t2 = t+x;
    return float2(t1, t2 - t1);
}

// Returns min/max intersection distances
float2 RayRectIntersection(float3 rayOrigin, float3 inverseRayDirection, int3 sign, float3 bounds[2]) {

    float tmin, tmax, tymin, tymax, tzmin, tzmax;
    tmin = (bounds[sign[0]].x - rayOrigin.x) * inverseRayDirection.x;
    tmax = (bounds[1 - sign[0]].x - rayOrigin.x) * inverseRayDirection.x;
    tymin = (bounds[sign[1]].y - rayOrigin.y) * inverseRayDirection.y;
    tymax = (bounds[1 - sign[1]].y - rayOrigin.y) * inverseRayDirection.y;

    if ((tmin > tymax) || (tymin > tmax))
        return float2(0,0);
    if (tymin > tmin)
        tmin = tymin;
    if (tymax < tmax)
        tmax = tymax;

    tzmin = (bounds[sign[2]].z - rayOrigin.z) * inverseRayDirection.z;
    tzmax = (bounds[1 - sign[2]].z - rayOrigin.z) * inverseRayDirection.z;

    if ((tmin > tzmax) || (tzmin > tmax))
        return float2(0,0);
    if (tzmin > tmin)
        tmin = tzmin;
    if (tzmax < tmax)
        tmax = tzmax;

    return float2(tmin,tmax);
}

#endif