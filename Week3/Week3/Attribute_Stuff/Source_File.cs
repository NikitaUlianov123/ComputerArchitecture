using System;
using System.Collections.Generic;
using System.Text;
using Week3.Cp_Stuff;

namespace Week3.Attribute_Stuff
{
    public class Source_File : Attribute_Info
    {
        public ushort sourcefile_index;

        public override void Parse(ref Memory<byte> hexdump, Constant_Pool pool)
        {
            sourcefile_index = hexdump.Read2();
        }
    }
}
