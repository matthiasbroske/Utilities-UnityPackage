#ifndef SHADER_UTILITIES_INCLUDED
#define SHADER_UTILITIES_INCLUDED

#define INFINITY 1e34

float Remap01(float dataValue, float2 dataRange)
{
    return (dataValue - dataRange.x) / (dataRange.y - dataRange.x);
}

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

#endif