using ClassLibrary;
using ClassLibrary.Instructions;
using ClassLibrary.Instructions.Flow_Control;
using ClassLibrary.Instructions.Logic;
using ClassLibrary.Instructions.Math;
using ClassLibrary.Instructions.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulatorMMIO
{
    public partial class Form1 : Form
    {
        public Computer computer;
        int scale = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void screenButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            Bitmap map = new Bitmap(32, 32);

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    map.SetPixel(x, y, byteToColor(computer.MMIO.Span[x + (y * 32)]));
                }
            }

            Screen.Image = Scale(map);
        }

        public Color byteToColor(byte Byte)
        {
            byte red = (byte)(Byte >> 5);
            byte green = (byte)(Byte << 3);
            green = (byte)(green >> 5);
            byte blue = (byte)(Byte << 6);
            blue = (byte)(blue >> 6);

            red *= (255 / 7);
            green *= (255 / 7);
            blue *= (255 / 3);

            return Color.FromArgb(red, green, blue);
        }

        public Bitmap Scale(Bitmap original)
        {
            Bitmap map = new Bitmap(320, 320);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color color = original.GetPixel(x, y);

                    for (int x1 = 0; x1 < scale; x1++)
                    {
                        for (int y1 = 0; y1 < scale; y1++)
                        {
                            map.SetPixel(x * scale + x1, y * scale + y1, color);
                        }
                    }
                }
            }

            return map;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            string[] code = new string[]
            {
                "Set R5 00f0",
                "Set R6 fbfd",
                "Set R8 fbfe",
                "Set R7 fff5",
                "Set R4 8",
                "Loop:",
                "Stori R5 R6",
                "Stori R5 R8",
                "Add R6 R4 R6",
                "Add R8 R4 R8",
                "Eq R6 R7 R10",
                "Breq R10 End",
                "Goto Loop",
                "End:"
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

            computer = new Computer();

            computer.LoadProgram(MachineCode);

            computer.Run();
        }

        private void dogButton_Click(object sender, EventArgs e)
        {
            Screen.Image = Properties.Resources.dog;
        }
    }
}
