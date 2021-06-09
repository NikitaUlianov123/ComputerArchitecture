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

        static void Main(string[] args)
        {
            Console.WriteLine(IsPowerOf2(1));
            Console.WriteLine(IsPowerOf2(0));
            Console.WriteLine(IsPowerOf2(7));
            Console.WriteLine(IsPowerOf2(16));
            Console.WriteLine(IsPowerOf2(64));
            Console.WriteLine(IsPowerOf2(3));
            Console.WriteLine(IsPowerOf2(27));
            Console.WriteLine(IsPowerOf2(5));
        }
    }
}
