using System;
using RosMessageTypes.Sensor;
using Unity.Robotics.MessageVisualizers;
using UnityEngine;

public class DefaultVisualizerPointField : DrawingVisualFactory<PointFieldMsg>
{
    public override Action CreateGUI(PointFieldMsg message, MessageMetadata meta)
    {
        return () =>
        {
            message.GUI();
        };
    }
}