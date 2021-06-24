using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Cp_Stuff
{
    public class CP_Utf8 : CP_Info
    {
        public override byte Tag => 0x01;

        public ushort Length;
        public byte[] bytes;

        public override void Parse(ref Memory<byte> hexdump)
        {
            Length = hexdump.Read2();
            bytes = new byte[Length];

            for (int i = 0; i < Length; i++)
            {
                bytes[i] = hexdump.Read1();
            }
        }

        public override string ToString()
        {
            return new string(Array.ConvertAll(bytes, new Converter<byte, char>(x => (char)x)));
        }
    }
}