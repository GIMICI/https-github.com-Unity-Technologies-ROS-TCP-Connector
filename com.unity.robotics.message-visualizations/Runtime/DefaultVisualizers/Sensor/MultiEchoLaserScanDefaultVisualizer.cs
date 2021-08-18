using System;
using RosMessageTypes.Sensor;
using Unity.Robotics.MessageVisualizers;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;

public class MultiEchoLaserScanDefaultVisualizer : DrawingVisualizerWithSettings<MultiEchoLaserScanMsg, MultiEchoLaserScanVisualizerSettings>
{
    public override string DefaultScriptableObjectPath => "VisualizerSettings/MultiEchoLaserScanVisualizerSettings";

    public override void Draw(BasicDrawing drawing, MultiEchoLaserScanMsg message, MessageMetadata meta)
    {
        Draw<FLU>(message, drawing, Settings);
    }

    public static void Draw<C>(MultiEchoLaserScanMsg message, BasicDrawing drawing, MultiEchoLaserScanVisualizerSettings settings)
        where C : ICoordinateSpace, new()
    {
        Draw<C>(message, drawing.AddPointCloud(message.ranges.Length), settings);
    }

    public static void Draw<C>(MultiEchoLaserScanMsg message, PointCloudDrawing pointCloud, MultiEchoLaserScanVisualizerSettings settings) where C : ICoordinateSpace, new()
    {
        pointCloud.SetCapacity(message.ranges.Length * message.ranges[0].echoes.Length);
        TFFrame frame = TFSystem.instance.GetTransform(message.header);
        // negate the angle because ROS coordinates are right-handed, unity coordinates are left-handed
        float angle = -message.angle_min;
        // foreach(MLaserEcho echo in message.ranges)
        for (int i = 0; i < message.ranges.Length; i++)
        {
            var echoes = message.ranges[i].echoes;
            // foreach (float range in echo.echoes)
            for (int j = 0; j < echoes.Length; j++)
            {
                Vector3 localPoint = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * Vector3.forward * echoes[j];
                Vector3 worldPoint = frame.TransformPoint(localPoint);
                Color c = Color.HSVToRGB(Mathf.InverseLerp(message.range_min, message.range_max, echoes[j]), 1, 1);

                var radius = settings.m_PointRadius;

                if (message.intensities.Length > 0 && settings.m_UseIntensitySize)
                {
                    radius = Mathf.InverseLerp(settings.m_SizeRange[0], settings.m_SizeRange[1], message.intensities[i].echoes[j]);
                }

                pointCloud.AddPoint(worldPoint, c, radius);
            }
            angle -= message.angle_increment;
        }
    }
}