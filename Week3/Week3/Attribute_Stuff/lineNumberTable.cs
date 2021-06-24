using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Attribute_Stuff
{
    public class lineNumberTable
    {
        public ushort start_pc;
        public ushort line_number;

        public void Parse(ref Memory<byte> hexdump)
        {
            start_pc = hexdump.Read2();
            line_number = hexdump.Read2();
        }
    }
}
