//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Tf2
{
    public class MTFMessage : Message
    {
        public const string RosMessageName = "tf2_msgs/TFMessage";

        public Geometry.MTransformStamped[] transforms;

        public MTFMessage()
        {
            this.transforms = new Geometry.MTransformStamped[0];
        }

        public MTFMessage(Geometry.MTransformStamped[] transforms)
        {
            this.transforms = transforms;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            
            listOfSerializations.Add(BitConverter.GetBytes(transforms.Length));
            foreach(var entry in transforms)
                listOfSerializations.Add(entry.Serialize());

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            
            var transformsArrayLength = DeserializeLength(data, offset);
            offset += 4;
            this.transforms= new Geometry.MTransformStamped[transformsArrayLength];
            for(var i = 0; i < transformsArrayLength; i++)
            {
                this.transforms[i] = new Geometry.MTransformStamped();
                offset = this.transforms[i].Deserialize(data, offset);
            }

            return offset;
        }

        public override string ToString()
        {
            return "MTFMessage: " +
            "\ntransforms: " + System.String.Join(", ", transforms.ToList());
        }
    }
}
