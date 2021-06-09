using System;

namespace ConsoleApp1
{
    public class Program
    {
        public static int NearestPowerOf2(int number)
        {
            if (IsPowerOf2(number))
            {
                return number;
            }

            if (number <= 1)
            {
                return 1;
            }

            int counter = number;

            while (!IsPowerOf2(counter))
            {
                counter = counter >> 1;
            }

            int numberLess = counter;

            int numberGreater = counter << 1;

            if (numberGreater != 0)
            {
                if (Math.Abs(numberLess - number) < numberGreater - number)
                {
                    return numberLess;
                }

                return numberGreater;
            }

            return numberLess;
        }

        public static bool IsPowerOf2(int number)
        {
            if (number == 1)
            {
                return true;
            }
            if (number == 0)
            {
                return false;
            }

            var thing = number >> 1;
            var thingy = thing << 1;

            return number == thingy;
        }


        public static byte GetNthByte(uint number, int byteIndex)
        {
            int mask = 0xFF;
            int bitIndex = byteIndex * 8;
            mask = mask << bitIndex;

            return (byte)((number & mask) >> bitIndex);
        }

        public static byte[] GetBytes(uint number)
        {
            byte[] array = { GetNthByte(number, 3), GetNthByte(number, 2), GetNthByte(number, 1), GetNthByte(number, 0) };

            return array;
        }

        public static int SetBit(int number, int bit, bool whatToSetItTo)
        {
            int mask;
            if (whatToSetItTo)
            {
                mask = 1;
            }
            else
            {
                mask = 0;
            }
            mask = mask << bit - 1;

            if ((number & mask) == mask)
            {
                return number;
            }

            number |= mask;

            return number;
        }


        public static int DoMath(uint data)
        {
            byte[] bytes = GetBytes(data);

            switch (bytes[0])
            {
                case 1:
                    return bytes[1] + bytes[2];
                    break;

                case 2:
                    int negative = bytes[2] * -1;
                    return bytes[1] + negative;
                    break;

                case 3:
                    int product = 0;
                    for (int i = 0; i < bytes[1]; i++)
                    {
                        product += bytes[2];
                    }
                    return product;
                    break;

                case 4:
                    int quotient = 0;
                    int remainder = bytes[1];
                    while (remainder >= bytes[2])
                    {
                        remainder -= bytes[2];
                        quotient++;
                    }
                    return quotient;
                    break;
            }

            return 0;
        }

        static void Main(string[] args)
        {
            var test = DoMath(0x03050300);
        }
    }
}
