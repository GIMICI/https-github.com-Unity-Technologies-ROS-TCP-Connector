//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;
using RosMessageTypes.Std;

namespace RosMessageTypes.Nav
{
    public class MGridCells : Message
    {
        public const string RosMessageName = "nav_msgs/GridCells";

        // an array of cells in a 2D grid
        public MHeader header;
        public float cell_width;
        public float cell_height;
        public Geometry.MPoint[] cells;

        public MGridCells()
        {
            this.header = new MHeader();
            this.cell_width = 0.0f;
            this.cell_height = 0.0f;
            this.cells = new Geometry.MPoint[0];
        }

        public MGridCells(MHeader header, float cell_width, float cell_height, Geometry.MPoint[] cells)
        {
            this.header = header;
            this.cell_width = cell_width;
            this.cell_height = cell_height;
            this.cells = cells;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.AddRange(header.SerializationStatements());
            listOfSerializations.Add(BitConverter.GetBytes(this.cell_width));
            listOfSerializations.Add(BitConverter.GetBytes(this.cell_height));

            listOfSerializations.Add(BitConverter.GetBytes(cells.Length));
            foreach (var entry in cells)
                listOfSerializations.Add(entry.Serialize());

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            offset = this.header.Deserialize(data, offset);
            this.cell_width = BitConverter.ToSingle(data, offset);
            offset += 4;
            this.cell_height = BitConverter.ToSingle(data, offset);
            offset += 4;

            var cellsArrayLength = DeserializeLength(data, offset);
            offset += 4;
            this.cells = new Geometry.MPoint[cellsArrayLength];
            for (var i = 0; i < cellsArrayLength; i++)
            {
                this.cells[i] = new Geometry.MPoint();
                offset = this.cells[i].Deserialize(data, offset);
            }

            return offset;
        }

        public override string ToString()
        {
            return "MGridCells: " +
            "\nheader: " + header.ToString() +
            "\ncell_width: " + cell_width.ToString() +
            "\ncell_height: " + cell_height.ToString() +
            "\ncells: " + System.String.Join(", ", cells.ToList());
        }
    }
}
