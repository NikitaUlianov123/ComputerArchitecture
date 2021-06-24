using System;
using System.Collections.Generic;
using System.Text;
using Week3.Cp_Stuff;

namespace Week3.Attribute_Stuff
{
    public abstract class Attribute_Info
    {
        public ushort attribute_name_index { get; protected set; }
        public uint attribute_length { get; protected set; }

        public abstract void Parse(ref Memory<byte> hexdump, Constant_Pool pool);
    }
}
