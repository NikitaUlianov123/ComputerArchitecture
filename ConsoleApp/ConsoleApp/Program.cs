using System;
using System.Collections.Generic;
using ClassLibrary;
using ClassLibrary.Instructions;
using ClassLibrary.Instructions.Math;
using ClassLibrary.Instructions.Memory;
using ClassLibrary.Instructions.Logic;
using ClassLibrary.Instructions.Flow_Control;
using System.Text.RegularExpressions;

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
               //Memory:
                new Set(),
                new Push(),
                new Pull(),
                new Store(),
                new Load(),
               //Math:
                new Add(),
                new Sub(),
                new Mul(),
                new Div(),
                new Mod(),
               //Logic:
                new And(),
                new Equal(),
                new Not(),
                new Or(),
                new SHL(),
                new SHR(),
                new Xor(),
               //Flow Control:
                new Breq(),
                new GoTo(),
                new GoToi(),
                new Nop(),
            };

            //label string to line offset
            Dictionary<string, int> Labels = new Dictionary<string, int>();

            string label = @"'^(\w+):";

            //First pass:
            for (int i = 0; i < assembly.Length; i++)
            {
                var match = Regex.Match(assembly[i], label);
                if (match.Success)
                {
                    Labels.Add(Convert.ToString(match.Groups[0]), i);
                }
            }

            List<Instruction> instructions = new List<Instruction>();
            List<byte> machineCode = new List<byte>();
            
            //Second pass:
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
