using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Instructions
{
    public class Iconst_5 : Instruction
    {
        public override byte OpCode => 0x08;

        public override void Parse(ref Memory<byte> hexdump)
        {
            throw new NotImplementedException();
        }
    }
}