using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Instructions
{
    public abstract class Instruction
    {
        public abstract byte OpCode { get; }

        public abstract void Parse(ref Memory<byte> hexdump);
    }
}
