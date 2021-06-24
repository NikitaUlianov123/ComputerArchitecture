using System;
using System.Collections.Generic;
using System.Text;
using Week3.Attribute_Stuff;
using Week3.Cp_Stuff;

namespace Week3.Field_Stuff
{
    public class Field_Info
    {
        public ushort access_flags {get; private set;}
        public ushort name_index { get; private set; }
        public ushort descriptor_index { get; private set; }
        public ushort attributes_count { get; private set; }
        public Attribute_Info[] attributes { get; private set; } //of size attributes_count

        public void Parse(ref Memory<byte> hexdump, Constant_Pool pool)
        {
            access_flags = hexdump.Read2();
            name_index = hexdump.Read2();
            descriptor_index = hexdump.Read2();
            attributes_count = hexdump.Read2();

            attributes = new Attribute_Info[attributes_count];
            for (int i = 0; i < attributes_count; i++)
            {
                var tag = hexdump.Read2();

                CP_Utf8 Utf8 = (CP_Utf8)pool.cp_info[tag - 1];

                string utf8 = Utf8.ToString();

                Attribute_Info yeet = null;

                switch (utf8)
                {
                    case "Code":
                        yeet = new Code();
                        break;

                    case "LineNumberTable":
                        yeet = new Line_Number_Table();
                        break;

                    case "SourceFile":
                        yeet = new Source_File();
                        break;
                }


                yeet.Parse(ref hexdump, pool);

                attributes[i] = yeet;
            }
        }
    }
}
