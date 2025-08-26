#ifndef SEGMENTED_SLIDER_VS
#define SEGMENTED_SLIDER_VS

layout(location = 0) in highp vec2 m_Position;
layout(location = 1) in highp vec2 m_SegmentPosition;
layout(location = 2) in highp float m_Rho;

layout(location = 0) out highp vec2 v_SegmentPosition;
layout(location = 1) out highp float v_Rho;

void main(void)
{
    // Centre v_SegmentPosition on the origin
    v_SegmentPosition = vec2(m_SegmentPosition.x - m_Rho * 0.5, m_SegmentPosition.y);
    v_Rho = m_Rho;

    gl_Position = g_ProjMatrix * vec4(m_Position, 1.0, 1.0);
}

#endif
