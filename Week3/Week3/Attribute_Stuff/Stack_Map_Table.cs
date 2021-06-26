using System;
using System.Collections.Generic;
using System.Text;
using Week3.Cp_Stuff;

namespace Week3.Attribute_Stuff
{
    public class Stack_Map_Table : Attribute_Info
    {
        ushort number_of_entries;
        Stack_Map_Frame entries[number_of_entries];

        public override void Parse(ref Memory<byte> hexdump, Constant_Pool pool)
        {
            attribute_name_index = hexdump.Read2();
            attribute_length = hexdump.Read4();
            number_of_entries = hexdump.Read2();

        }
    }
}