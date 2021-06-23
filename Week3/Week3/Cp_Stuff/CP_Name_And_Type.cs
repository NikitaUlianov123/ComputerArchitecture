using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Cp_Stuff
{
    public class CP_Name_And_Type : CP_Info
    {
        public override byte Tag => 0x0c;

        public ushort Name_Index;
        public ushort Descriptor_Index;

        public override void Parse(ref Memory<byte> hexdump)
        {
            Name_Index = hexdump.Read2();
            Descriptor_Index = hexdump.Read2();
        }
    }
}
