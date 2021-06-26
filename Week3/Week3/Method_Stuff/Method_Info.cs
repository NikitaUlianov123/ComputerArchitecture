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

        public uint? Execute(Constant_Pool pool, Method_Info[] methods)
        {
            var codeAttribute = (Code)attributes[0];
            var code = codeAttribute.code;
            if (locals == null)
            {
                locals = new uint?[codeAttribute.max_locals];
            }
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

                    case 0x1a: //iload_0
                        Program.stack.Push(locals[0]);
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

                    case 0xb8: //invokestatic
                        ushort index = (ushort)((code[i + 1] << 8) + (code[i + 2]));
                        var yeet = (CP_MethodRef_Info)(pool.cp_info[index - 1]);
                        var yote = (CP_Name_And_Type)(pool.cp_info[yeet.Name_And_Type_Index - 1]);

                        Method_Info Method = null;

                        for (int j = 0; j < methods.Length; j++)
                        {
                            if (methods[j].name_index == yote.Name_Index && methods[j].descriptor_index == yote.Descriptor_Index)
                            {
                                Method = methods[j];
                            }
                        }

                        var CodeAttribute = (Code)(Method.attributes[0]);
                        Method.locals = new uint?[CodeAttribute.max_locals];
                        for (int k = 0; k < Method.locals.Length; k++)
                        {
                            Method.locals[k] = Program.stack.Pop();
                        }
                        Program.stack.Push(Method.Execute(pool, methods));
                        break;

                    case 0xac: //return
                        return Program.stack.Pop();
                        break;

                    case 0xb1: //return
                        return null;
                        break;

                    default:
                        throw new Exception("OOF");
                        break;
                }
            }

            return null;
        }
    }
}
