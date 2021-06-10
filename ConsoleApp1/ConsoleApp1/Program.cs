using System;
using System.Collections.Generic;

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

        public unsafe static bool IsPalindrome(string s)
        {
            fixed(char* start = s)
            {
                char* end = start + s.Length - 1;

                for (int i = 0; i < s.Length / 2; i++)
                {
                    char* current = start + i;
                    char* otherCurrent = end - i;

                    if (*current != *otherCurrent)
                    {
                        return false;
                    }
                }

                return true;
            }
        }


        public static Stack<int> numberStack = new Stack<int>();

        public static int DoMath(uint data)
        {
            byte[] bytes = GetBytes(data);

            switch (bytes[0])
            {
                case 1:
                    numberStack.Push(bytes[1]);
                    numberStack.Push(bytes[2]);
                    Add();
                    return numberStack.Pop();
                    break;

                case 2:
                    numberStack.Push(bytes[1]);
                    numberStack.Push(bytes[2]);
                    Subtract();
                    return numberStack.Pop();
                    break;

                case 3:
                    numberStack.Push(bytes[1]);
                    numberStack.Push(bytes[2]);
                    Mulitply();
                    return numberStack.Pop();
                    break;

                case 4:
                    numberStack.Push(bytes[1]);
                    numberStack.Push(bytes[2]);
                    Divide();
                    return numberStack.Pop();
                    break;
            }

            return 0;
        }

        public static void Add()
        {
            int number1 = numberStack.Pop();
            int number2 = numberStack.Pop();
            numberStack.Push(number1 + number2);
        }

        public static void Subtract()
        {
            int number1 = numberStack.Pop();
            int number2 = numberStack.Pop();
            number1 *= -1;
            numberStack.Push(number1 + number2);
        }

        public static void Mulitply()
        {
            int number1 = numberStack.Pop();
            int number2 = numberStack.Pop();
            int product = 0;
            for (int i = 0; i < number1; i++)
            {
                product += number2;
            }
            numberStack.Push(product);
        }

        public static void Divide()
        {
            int number1 = numberStack.Pop();
            int number2 = numberStack.Pop();
            int quotient = 0;
            int remainder = number2;
            while (remainder >= number1)
            {
                remainder -= number1;
                quotient++;
            }
            numberStack.Push(quotient);
        }

        static void Main(string[] args)
        {
            
        }
    }
}
