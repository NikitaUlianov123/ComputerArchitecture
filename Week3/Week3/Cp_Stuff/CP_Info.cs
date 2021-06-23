using System;
using System.Collections.Generic;
using System.Text;

namespace Week3
{
    public abstract class CP_Info
    {
        public abstract byte Tag { get; }

        public abstract void Parse(ref Memory<byte> hexdump);
    }
}
