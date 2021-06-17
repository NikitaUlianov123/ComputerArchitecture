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
                "FlowControl:",
                "BREQ R7 ABCD",
                "Breq R3 FlowControl",
                "GoTo f00d",
                "Gotoi R10",
                "Goto Logic",
                "Nop",
                "Logic:",
                "And R1 R2 R3",
                "Eq R3 R4 R5",
                "Not R5 R6",
                "Or R10 R11 R12",
                "SHL R17 R18",
                "SHR R18 R17",
                "Xor R19 R20 R21",
                "Math:",
                "Add R1 R2 R3",
                "Sub R1 R2 R3",
                "Mul R1 R2 R3",
                "Div R1 R2 R3",
                "Mod R1 R2 R3",
                "Memory:",
                "Set R1 30",
                "Push R22",
                "Pull R23",
                "Store R5 f00d",
                "Load R25 f00d"
            };

            List<Instruction> possibleInstructions
           = new List<Instruction>()
            {
               //Flow Control:
                new Breq(),
                new BreqLabel(),
                new GoTo(),
                new GoToi(),
                new GoToLabels(),
                new Nop(),
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
            };

            //label string to line offset
            Dictionary<string, int> Labels = new Dictionary<string, int>();

            string label = @"^(\w+):";

            ushort offset = 0;
            //First pass:
            for (int i = 0; i < assembly.Length; i++)
            {
                var match = Regex.Match(assembly[i], label);
                if (match.Success)
                {
                    Labels.Add(Convert.ToString(match.Groups[1]), offset);
                }
                else
                {
                    offset += 4;
                }
            }

            List<Instruction> instructions = new List<Instruction>();
            List<byte> machineCode = new List<byte>();
            
            //Second pass:
            foreach (var line in assembly)
            {
                Console.WriteLine(line);
                var match = Regex.Match(line, label);
                if (match.Success) continue;
                bool valid = false;
                foreach (var possibleInstruction in possibleInstructions)
                {
                    if (!(possibleInstruction.Parse(line) is Instruction instruction)) continue;
                    valid = true;

                    if (possibleInstruction.IsGoToLabels())
                    {
                        GoTo instruction2 = new GoTo();
                        instruction2.memAddress = ushort.Parse(Labels[((GoToLabels)possibleInstruction).Label].ToString());

                        instructions.Add(instruction2);
                    }
                    else if (possibleInstruction.IsBreqLabel())
                    {
                        Breq instruction2 = new Breq();
                        instruction2.checkReg = ((BreqLabel)possibleInstruction).checkReg;
                        instruction2.memaddress = ushort.Parse(Labels[((BreqLabel)possibleInstruction).Label].ToString());

                        instructions.Add(instruction2);
                    }
                    else
                    {

                        instructions.Add(possibleInstruction);
                    }


                    var bytes = instructions[instructions.Count - 1].Emit();
                    machineCode.AddRange(bytes);

                    foreach (var @byte in bytes)
                    {
                        Console.Write($"{@byte:X2} ");
                    }
                    Console.WriteLine("\n");
                }

                if (!valid)
                {
                    Console.WriteLine("No");
                    Console.WriteLine("");
                }
            }
        }
    }
}
