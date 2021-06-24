using System;
using System.Collections.Generic;
using System.Text;
using Week3.Cp_Stuff;

namespace Week3.Attribute_Stuff
{
    public class Line_Number_Table : Attribute_Info
    {
        public ushort line_number_table_length;
        public lineNumberTable[] lineNumbers; //of size line_number_table_length

        public override void Parse(ref Memory<byte> hexdump, Constant_Pool pool)
        {
            attribute_length = hexdump.Read4();
            line_number_table_length = hexdump.Read2();

            lineNumbers = new lineNumberTable[line_number_table_length];
            for (int i = 0; i < line_number_table_length; i++)
            {
                var yeet = new lineNumberTable();
                yeet.Parse(ref hexdump);

                lineNumbers[i] = yeet;
            }
        }
    }
}
