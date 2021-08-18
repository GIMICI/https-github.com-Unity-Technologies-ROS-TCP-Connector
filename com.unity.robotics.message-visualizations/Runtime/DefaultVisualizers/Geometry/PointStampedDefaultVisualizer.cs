using System;
using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;

namespace Unity.Robotics.MessageVisualizers
{
    public class PointStampedDefaultVisualizer : DrawingVisualizer<PointStampedMsg>
    {
        [SerializeField]
        float m_Radius = 0.01f;
        [SerializeField]
        Color m_Color;
        [SerializeField]
        string m_Label;
        [SerializeField]
        TFTrackingType m_TFTrackingType;

        public override void Draw(BasicDrawing drawing, PointStampedMsg message, MessageMetadata meta)
        {
            drawing.SetTFTrackingType(m_TFTrackingType, message.header);
            PointDefaultVisualizer.Draw<FLU>(message.point, drawing, SelectColor(m_Color, meta), SelectLabel(m_Label, meta), m_Radius);
        }

        public override Action CreateGUI(PointStampedMsg message, MessageMetadata meta)
        {
            return () =>
            {
                message.header.GUI();
                message.point.GUI();
            };
        }
    }
}