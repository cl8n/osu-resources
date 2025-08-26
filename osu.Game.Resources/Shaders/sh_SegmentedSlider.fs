#ifndef SEGMENTED_SLIDER_FS
#define SEGMENTED_SLIDER_FS

layout(location = 0) in highp vec2 v_SegmentPosition;
layout(location = 1) in highp float v_Rho;

layout(std140, set = 0, binding = 0) uniform m_SliderParameters
{
    lowp vec4 colour;
    bool border;
};

layout(location = 0) out vec4 o_Colour;

// Gets the distance between pos and a horizontal line segment at the origin with length rho.
highp float segmentSdf(highp vec2 pos, highp float rho)
{
    pos.x = max(abs(pos.x) - rho * 0.5, 0.0);

    return length(pos);
}

void main(void)
{
    const highp float fadeStart = 0.95;

    // Fade out edges to create the segment shape
    highp float segmentDistance = segmentSdf(v_SegmentPosition, v_Rho);
    lowp float segmentAlpha = (1.0 - segmentDistance) / (1.0 - fadeStart);

    if (border)
    {
        o_Colour = vec4(colour.rgb, segmentAlpha);
        return;
    }

    // Set gradient that gets less bright and more opaque toward the edges
    highp float absY = abs(v_SegmentPosition.y);
    lowp vec3 gradientColour = colour.rgb + (0.2 - 0.25 * absY);
    lowp float gradientAlpha = segmentDistance < fadeStart ? 0.9 + 0.25 * absY : segmentAlpha;

    o_Colour = vec4(gradientColour, gradientAlpha);
}

#endif
