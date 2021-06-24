using System;
using System.Collections.Generic;
using System.Text;
using Week3.Cp_Stuff;

namespace Week3.Attribute_Stuff
{
    public class Code : Attribute_Info
    {
        public ushort max_stack { get; private set; }
        public ushort max_locals { get; private set; }
        public uint code_length { get; private set; }
        public byte[] code { get; private set; } //of size code_length
        public ushort exception_table_length { get; private set; }

        public Exception_Table[] exceptions { get; private set; } //os size exception_table_length
        public ushort attributes_count { get; private set; }
        public Attribute_Info[] attributes { get; private set; } //of size attributes_count

        public override void Parse(ref Memory<byte> hexdump, Constant_Pool pool)
        {
            attribute_length = hexdump.Read4();

            max_stack = hexdump.Read2();
            max_locals = hexdump.Read2();
            code_length = hexdump.Read4();

            code = new byte[code_length];
            for (int i = 0; i < code_length; i++)
            {
                code[i] = hexdump.Read1();
            }

            exception_table_length = hexdump.Read2();
            exceptions = new Exception_Table[exception_table_length];
            for (int i = 0; i < exception_table_length; i++)
            {
                var yeet = new Exception_Table();
                yeet.Parse(ref hexdump);

                exceptions[i] = yeet;
            }

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