using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Week3.Attribute_Stuff;
using Week3.Cp_Stuff;
using Week3.Field_Stuff;
using Week3.Method_Stuff;

namespace Week3
{
    class Program
    {
        static CP_Info[] GetAllTypesThatInheritCpInfo()
            => Assembly.GetAssembly(typeof(CP_Info)).GetTypes()
                                                    .Where(x => x.IsSubclassOf(typeof(CP_Info)))
                                                    .Select(x => (CP_Info)Activator.CreateInstance(x)).ToArray();

        static void Main(string[] args)
        {
            #region ClassFileFormat_Parsing
            CP_Info[] allConstantPoolItems = GetAllTypesThatInheritCpInfo();

            Dictionary<byte, Func<CP_Info>> map = new Dictionary<byte, Func<CP_Info>>();
            foreach (var item in allConstantPoolItems)
            {
                map.Add(item.Tag, new Func<CP_Info>(() =>
                {
                    return (CP_Info)Activator.CreateInstance(item.GetType());
                }));
            }



            byte[] bytes = System.IO.File.ReadAllBytes("Program.class");

            Memory<byte> hexdump = bytes.AsMemory();

            uint magic = hexdump.Read4();
            ushort minor_version = hexdump.Read2();
            ushort major_version = hexdump.Read2();
            ushort constant_pool_count = hexdump.Read2();

            Constant_Pool pool = new Constant_Pool(constant_pool_count - 1);

            for (int i = 0; i < constant_pool_count - 1; i++)
            {
                byte tag = hexdump.Read1();

                CP_Info currentInfo = map[tag]();

                currentInfo.Parse(ref hexdump);

                pool.Set(currentInfo);
            }

            ushort Access_Flags = hexdump.Read2();

            ushort This_Class = hexdump.Read2();

            ushort Super_Class = hexdump.Read2();

            ushort Interfaces_Count = hexdump.Read2();

            ushort[] Interfaces = new ushort[Interfaces_Count];
            for (int i = 0; i < Interfaces_Count; i++)
            {
                Interfaces[i] = hexdump.Read2();
            }

            ushort Fields_Count = hexdump.Read2();
            Field_Info[] fields = new Field_Info[Fields_Count];
            for (int i = 0; i < Fields_Count; i++)
            {
                Field_Info field = new Field_Info();
                field.Parse(ref hexdump);

                fields[i] = field;
            }

            ushort Methods_Count = hexdump.Read2();
            Method_Info[] methods = new Method_Info[Methods_Count];
            for (int i = 0; i < Methods_Count; i++)
            {
                Method_Info method = new Method_Info();
                method.Parse(ref hexdump);

                methods[i] = method;
            }

            ushort Attributes_Count = hexdump.Read2();
            Attribute_Info[] attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes_Count; i++)
            {
                Attribute_Info attribute = new Attribute_Info();
                attribute.Parse(ref hexdump);

                attributes[i] = attribute;
            }
            #endregion

            //Finding main:
            Method_Info mainMethod = null;
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].access_flags == 0x0009)
                {
                    var descriptor = (CP_Utf8)pool.cp_info[methods[i].descriptor_index - 1];
                    byte[] byties;

                    var yeet = (CP_Utf8)pool.cp_info[methods[i].name_index - 1];

                    byties = yeet.bytes;

                    string name = new string(byties.Select((x) => (char)x).ToArray());


                    var yaat = descriptor.bytes;
                    string Descriptor = new string(Array.ConvertAll(yaat, new Converter<byte, char>(x => (char)x)));

                    if (name == "main" && Descriptor == "([Ljava/lang/String;)V")
                    {
                        mainMethod = methods[i];
                    }
                }
            }

            //Finding code:
            var code = mainMethod.attributes[0].info;

            
        }
    }
}