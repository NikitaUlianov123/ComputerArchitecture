using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Cp_Stuff
{
    public class CP_Class : CP_Info
    {
        public override byte Tag => 0x07;
        public ushort Name_Index {get; private set;}

        public override void Parse(ref Memory<byte> hexdump)
        {
            Name_Index = hexdump.Read2();
        }
    }
}
