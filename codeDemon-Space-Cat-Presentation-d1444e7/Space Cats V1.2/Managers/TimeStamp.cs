using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Space_Cats_V1._2
{
    class TimeStamp
    {
        public enum TimeStampType
        {
            Absolute, Relative
        }
        private TimeStampType z_type;
        private int z_time;

        public TimeStampType Type
        {
            get { return z_type; }
            set { z_type = value; }
        }

        public int Time
        {
            get { return z_time; }
            set { z_time = value; }
        }

        public TimeStamp(TimeStampType type, int time)
        {
            z_type = type;
            z_time = time;
        }

        public TimeStamp(BinaryReader br)
        {
            z_type = (TimeStampType)br.ReadInt32();
            z_time = br.ReadInt32();
        }

        public void WriteToFile(BinaryWriter bw)
        {
            bw.Write((int)z_type);
            bw.Write(z_time);
        }

    }
}
