using System;
using System.Collections.Generic;
using System.Text;
using Week3.Attribute_Stuff;

namespace Week3.Field_Stuff
{
    public class Field_Info
    {
        public ushort access_flags {get; private set;}
        public ushort name_index { get; private set; }
        public ushort descriptor_index { get; private set; }
        public ushort attributes_count { get; private set; }
        public Attribute_Info[] attributes { get; private set; } //of size attributes_count

        public void Parse(ref Memory<byte> hexdump)
        {
            access_flags = hexdump.Read2();
            name_index = hexdump.Read2();
            descriptor_index = hexdump.Read2();
            attributes_count = hexdump.Read2();

            attributes = new Attribute_Info[attributes_count];
            for (int i = 0; i < attributes_count; i++)
            {
                Attribute_Info info = new Attribute_Info();
                info.Parse(ref hexdump);

                attributes[i] = info;
            }
        }
    }
}
