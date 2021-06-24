using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Attribute_Stuff
{
    public class Exception_Table
    {
        public ushort start_pc;
        public ushort end_pc;
        public ushort handler_pc;
        public ushort catch_type;

        public void Parse(ref Memory<byte> hexdump)
        {
            start_pc = hexdump.Read2();
            end_pc = hexdump.Read2();
            handler_pc = hexdump.Read2();
            catch_type = hexdump.Read2();
        }
    }
}
