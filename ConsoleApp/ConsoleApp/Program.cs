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
            string[] code = new string[]
            {
                "Set R5 5",
                "Set R6 6",

                "Store R5 fff8",
                "Store R6 fffa",

                "Set R7 fff8",
                "Set R8 fffa",

                "Loadi R7 R3",
                "Loadi R8 R4",

                "Stori R4 R7",
                "Stori R3 R8"
            };

            string[] test1 = new string[]
            {
                "FlowControl:",
                "BREQ R7 abcd",
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

            string[] assembly = code;

            #region assembler
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
                new Copy(),
                new Storei(),
                new Loadi(),
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

            #endregion

            byte[] MachineCode = machineCode.ToArray();

            Computer computer = new Computer();

            computer.LoadProgram(MachineCode);

            computer.Run();

            #region dissassembler
            if (MachineCode.Length % 4 != 0) throw new Exception("What the hell did you give me?!?!?!?!?!?!?!");

            List<string> OriginalCode = new List<string>();

            for (int i = 0; i < MachineCode.Length; i += 4)
            {
                byte[] line = new byte[]
                {
                    MachineCode[i],
                    MachineCode[i + 1],
                    MachineCode[i + 2],
                    MachineCode[i + 3]
                };

                string lineOutput = "";

                switch (line[0])
                {
                    //Math:
                    case 0x10:
                        //Add
                        lineOutput = $"Add R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x11:
                        //Sub
                        lineOutput = $"Sub R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x12:
                        //Mul
                        lineOutput = $"Mul R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x13:
                        //Div
                        lineOutput = $"Div R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x14:
                        //Mod
                        lineOutput = $"Mod R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    //Logic:
                    case 0x20:
                        //SHL
                        lineOutput = $"SHL R{line[1]} R{line[2]}";
                        break;

                    case 0x21:
                        //SHR
                        lineOutput = $"SHR R{line[1]} R{line[2]}";
                        break;

                    case 0x22:
                        //And
                        lineOutput = $"And R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x23:
                        //Or
                        lineOutput = $"Or R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x24:
                        //Xor
                        lineOutput = $"Xor R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    case 0x25:
                        //Not
                        lineOutput = $"Not R{line[1]} R{line[2]}";
                        break;

                    case 0x26:
                        //Eq
                        lineOutput = $"Eq R{line[1]} R{line[2]} R{line[3]}";
                        break;

                    //Flow control:
                    case 0x30:
                        //Goto
                        lineOutput = $"GoTo {line[1]}{line[2]}";
                        break;

                    case 0x31:
                        //Gotoi
                        lineOutput = $"Gotoi R{line[1]}";
                        break;

                    case 0x35:
                        //Breq
                        lineOutput = $"Breq R{line[1]} {line[2]}{line[3]}";
                        break;

                    case 0x00:
                        //Nop
                        lineOutput = "Nop";
                        break;

                    //Memory:
                    case 0x40:
                        //Set
                        lineOutput = $"Set R{line[1]} {line[2]}{line[3]}";
                        break;

                    case 0x41:
                        //Push
                        lineOutput = $"Push R{line[1]}";
                        break;

                    case 0x42:
                        //Pull
                        lineOutput = $"Pull R{line[1]}";
                        break;

                    case 0x43:
                        //Store
                        lineOutput = $"Store R{line[1]} {line[2]}{line[3]}";
                        break;

                    case 0x44:
                        //Load
                        lineOutput = $"Load R{line[1]} {line[2]}{line[3]}";
                        break;

                    case 0x45:
                        //Copy
                        lineOutput = $"Copy R{line[1]} R{line[2]}";
                        break;
                }

                OriginalCode.Add(lineOutput);
            }
            #endregion
        }
    }
}