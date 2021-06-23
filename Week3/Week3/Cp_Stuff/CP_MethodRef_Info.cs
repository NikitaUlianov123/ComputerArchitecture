using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Cp_Stuff
{
    public class CP_MethodRef_Info : CP_Info
    {
        public override byte Tag => 0x0A;

        public ushort Class_Index{get; private set;}
        public ushort Name_And_Type_Index{get; private set;}

        public override void Parse(ref Memory<byte> hexdump)
        {
            Class_Index = hexdump.Read2();
            Name_And_Type_Index = hexdump.Read2();
        }
    }
}
