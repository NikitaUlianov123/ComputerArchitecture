using System;
using System.Collections.Generic;
using System.Text;
using Week3.Attribute_Stuff;
using Week3.Cp_Stuff;

namespace Week3.Method_Stuff
{
    public class Method_Info
    {
        public ushort access_flags { get; private set; }
        public ushort name_index { get; private set; }
        public ushort descriptor_index { get; private set; }
        public ushort attributes_count { get; private set; }
        public Attribute_Info[] attributes { get; private set; } //of size attributes_count

        public uint?[] locals { get; private set; }

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

        public void Execute()
        {
            var codeAttribute = (Code)attributes[0];
            var code = codeAttribute.code;
            locals = new uint?[codeAttribute.max_locals];
            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case 0x06: //iconst_3
                        Program.stack.Push(3);
                        break;

                    case 0x08: //iconst_5
                        Program.stack.Push(5);
                        break;

                    case 0x3c: //istore_1
                        locals[1] = Program.stack.Pop();
                        break;

                    case 0x3d: //istore_2
                        locals[2] = Program.stack.Pop();
                        break;

                    case 0x3e: //istore_3
                        locals[3] = Program.stack.Pop();
                        break;

                    case 0x1b: //iload_1
                        Program.stack.Push(locals[1]);
                        break;

                    case 0x1c: //iload_2
                        Program.stack.Push(locals[2]);
                        break;

                    case 0x60: //iadd
                        uint? one = Program.stack.Pop();
                        uint? two = Program.stack.Pop();
                        Program.stack.Push(one + two);
                        break;

                    case 0xb1: //return
                        return;
                        break;
                }
            }
        }
    }
}
