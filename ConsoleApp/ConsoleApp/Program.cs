using System;
using System.Collections.Generic;
using ClassLibrary;
using ClassLibrary.Instructions;
using ClassLibrary.Instructions.Math;
using ClassLibrary.Instructions.Mamory;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] assembly = new[]
            {
                "Set R0 42  //comment",
                "Set R1 50  //comment",
                "Add R0 R1 R2  //comment",
            };

            List<Instruction> possibleInstructions
           = new List<Instruction>()
            {
                new Set(),
                new Add()
            };

            /*
             Labels:

            have dictionary <string, byte offset>
             
             
             */



            List<Instruction> instructions = new List<Instruction>();
            List<byte> machineCode = new List<byte>();

            foreach (var line in assembly)
            {
                bool valid = false;
                foreach (var possibleInstruction in possibleInstructions)
                {
                    if (!(possibleInstruction.Parse(line) is Instruction instruction)) continue;
                    valid = true;

                    instructions.Add(possibleInstruction);

                    var bytes = instruction.Emit();
                    machineCode.AddRange(bytes);

                    foreach (var @byte in bytes)
                    {
                        Console.Write($"{@byte:X2} ");
                    }
                    Console.WriteLine("\n");
                }

                if (!valid)
                {
                    Console.WriteLine("Invalid assembly");
                    Console.WriteLine("");
                }
            }
        }
    }
}
