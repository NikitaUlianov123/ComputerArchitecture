using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Week3
{
    public static class Extensions
    {
        public static int Yolo(this string myString)
        {
            return int.Parse(myString);
        }


        public static byte Read1(this ref Memory<byte> hexdump) //u1
        {
            var ret = hexdump.Span[0]; //read byte at top

            hexdump = hexdump.Slice(1); //advance mmemory by 1 byte

            return ret; // return value that we read
        }

        public static ushort Read2(this ref Memory<byte> hexdump) //u2
        {
            var casted = MemoryMarshal.Cast<byte, ushort>(hexdump.Span);

            ushort ret = casted[0];

            ret = ret.ReverseBytes();

            hexdump = hexdump.Slice(2);

            return ret;
        }

        public static uint Read4(this ref Memory<byte> hexdump) //u4
        {
            var casted = MemoryMarshal.Cast<byte, uint>(hexdump.Span);

            uint ret = casted[0];

            ret = ret.ReverseBytes();

            hexdump = hexdump.Slice(4);

            return ret;
        }

        public static ushort ReverseBytes(this ushort item)
        {
            byte lowByte = (byte)item;
            byte highByte = (byte)(item >> 8);

            return (ushort)(lowByte << 8 | highByte);
        }

        public static uint ReverseBytes(this uint item)
        {
            ushort lowBytes = (ushort)(item);

            ushort highBytes = (ushort)(item >> 16);

            ushort reversedHighBytes = highBytes.ReverseBytes();
            ushort reversedLowBytes = lowBytes.ReverseBytes();

            return (uint)(reversedLowBytes << 16 | reversedHighBytes);
        }
    }
}