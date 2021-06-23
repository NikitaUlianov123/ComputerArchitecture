using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Attribute_Stuff
{
    public class Attribute_Info
    {
        public ushort attribute_name_index { get; private set; }
        public uint attribute_length { get; private set; }
        public byte[] info { get; private set; } //Of size attribute_length

        public void Parse(ref Memory<byte> hexdump)
        {
            attribute_name_index = hexdump.Read2();
            attribute_length = hexdump.Read4();

            info = new byte[attribute_length];
            for (int i = 0; i < attribute_length; i++)
            {
                info[i] = hexdump.Read1();
            }
        }
    }
}
