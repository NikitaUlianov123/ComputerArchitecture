using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Computer
    {
        private ushort[] registers;
        public byte[] memory;
        int stackStart = 31;
        int instructionPointer = 30;
        int stackPointer = 29;

        public Memory<byte> MMIO;

        public Computer()
        {
            registers = new ushort[32];
            memory = new byte[ushort.MaxValue];

            registers[stackStart] = 0xFF00;
            registers[instructionPointer] = 0x0000;
            registers[stackPointer] = registers[stackStart];

            MMIO = memory.AsMemory<byte>(memory.Length - 1025, 1024);
        }

        public void LoadProgram(byte[] program)
        {
            if (program.Length >= registers[stackStart]) throw new Exception("Program too big");
            if (program.Length % 4 != 0) throw new Exception("What the hell did you give me?!?!?!?!?!?!?!");
            foreach (byte @byte in program)
            {
                memory[registers[instructionPointer]] = @byte;
                registers[instructionPointer]++;
            }
            registers[instructionPointer] = 0;
        }

        public void Run()
        {
            while (registers[instructionPointer] < registers[stackStart])
            {
                byte[] line = new byte[]
                {
                    memory[registers[instructionPointer]],
                    memory[registers[instructionPointer] + 1],
                    memory[registers[instructionPointer] + 2],
                    memory[registers[instructionPointer] + 3]
                };

                switch (line[0])
                {
                    //Math:
                    case 0x10:
                        //Add
                        Add(line);
                        break;

                    case 0x11:
                        //Sub
                        Sub(line);
                        break;

                    case 0x12:
                        //Mul
                        Mul(line);
                        break;

                    case 0x13:
                        //Div
                        Div(line);
                        break;

                    case 0x14:
                        //Mod
                        Mod(line);
                        break;

                    //Logic:
                    case 0x20:
                        //SHL
                        SHL(line);
                        break;

                    case 0x21:
                        //SHR
                        SHR(line);
                        break;

                    case 0x22:
                        //And
                        And(line);
                        break;

                    case 0x23:
                        //Or
                        Or(line);
                        break;

                    case 0x24:
                        //Xor
                        Xor(line);
                        break;

                    case 0x25:
                        //Not
                        Not(line);
                        break;

                    case 0x26:
                        //Eq
                        Eq(line);
                        break;

                    //Flow control:
                    case 0x30:
                        //Goto
                        Goto(line);
                        continue; //so it doesn't add 4 to my newly changed instruction pointer.
                        break;

                    case 0x31:
                        //Gotoi
                        Gotoi(line);
                        continue; //so it doesn't add 4 to my newly changed instruction pointer.
                        break;

                    case 0x35:
                        //Breq
                        Breq(line);
                        break;

                    case 0x00:
                        //Nop
                        Nop(line);
                        break;

                    //Memory:
                    case 0x40:
                        //Set
                        Set(line);
                        break;

                    case 0x41:
                        //Push
                        Push(line);
                        break;

                    case 0x42:
                        //Pull
                        Pull(line);
                        break;

                    case 0x43:
                        //Store
                        Store(line);
                        break;

                    case 0x44:
                        //Load
                        Load(line);
                        break;

                    case 0x45:
                        //Copy
                        Copy(line);
                        break;

                    case 0x46:
                        //Loadi
                        Loadi(line);
                        break;

                    case 0x47:
                        //Storei
                        Storei(line);
                        break;
                }

                registers[instructionPointer] += 4;
            }
        }

        //Math Functions:
        private void Add(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] + registers[line[2]]);
        }
        private void Sub(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] - registers[line[2]]);
        }
        private void Mul(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] * registers[line[2]]);
        }
        private void Div(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] / registers[line[2]]);
        }
        private void Mod(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] % registers[line[2]]);
        }

        //Logic Functions:
        private void SHL(byte[] line)
        {
            registers[line[2]] = (ushort)(registers[line[1]] << 1);
        }
        private void SHR(byte[] line)
        {
            registers[line[2]] = (ushort)(registers[line[1]] >> 1);
        }
        private void And(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] & registers[line[2]]);
        }
        private void Or(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] | registers[line[2]]);
        }
        private void Xor(byte[] line)
        {
            registers[line[3]] = (ushort)(registers[line[1]] ^ registers[line[2]]);
        }
        private void Not(byte[] line)
        {
            //~
            registers[line[2]] = (ushort)(~registers[line[1]]);
        }
        private void Eq(byte[] line)
        {
            if (registers[line[1]] == registers[line[2]])
            {
                registers[line[3]] = 0x0001;
            }
            else
            {
                registers[line[3]] = 0x0000;
            }
        }

        //Flow control functions:
        private void Goto(byte[] line)
        {
            ushort address = (ushort)(line[1] << 8);
            address += line[2];
            registers[instructionPointer] = address;
        }
        private void Gotoi(byte[] line)
        {
            registers[instructionPointer] = registers[line[1]];
        }
        private void Breq(byte[] line)
        {
            if (registers[line[1]] == 0x0001)
            {
                ushort address = (ushort)(line[2] << 8);
                address += line[3];
                registers[instructionPointer] = (ushort)(address - 4);//because the for loop is going to add those 4 inediatly.
            }
        }
        private void Nop(byte[] line)
        {
            
        }

        //Memory functions:
        private void Set(byte[] line)
        {
            ushort value = (ushort)(line[2] << 8);
            value += line[3];
            registers[line[1]] = value;
        }
        private void Push(byte[] line)
        {
            memory[registers[stackPointer]] = (byte)(registers[line[1]] >> 8);
            memory[registers[stackPointer + 4]] = (byte)(registers[line[1]]);

            stackPointer += 8;
        }
        private void Pull(byte[] line)
        {
            registers[line[1]] = (ushort)(memory[registers[stackPointer]] << 8);
            registers[line[1]] += memory[registers[stackPointer + 4]];

            stackPointer -= 8;
        }
        private void Store(byte[] line)
        {
            ushort address = (ushort)(line[2] << 8);
            address += line[3];

            memory[address] = (byte)(registers[line[1]] >> 8);
            memory[address + 4] = (byte)(registers[line[1]]);
        }
        private void Load(byte[] line)
        {
            ushort address = (ushort)(line[2] << 8);
            address += line[3];

            registers[line[1]] = (ushort)(memory[address] << 8);
            registers[line[1]] += memory[address + 4];
        }
        private void Copy(byte[] line)
        {
            registers[line[2]] = registers[line[1]];
        }
        private void Loadi(byte[] line)
        {
            registers[line[2]] = memory[registers[line[1]]];
            registers[line[2]] |= memory[registers[line[1]] + 4];
        }
        private void Storei(byte[] line)
        {
            memory[registers[line[2]]] = (byte)(registers[line[1]] >> 8);
            memory[registers[line[2]] + 4] = (byte)(registers[line[1]]);
        }
    }
}