using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Memory
{
    public class Set : Instruction
    {
        private string originalAssembly;
        private byte destReg;
        private ushort val;

        protected override string Pattern
            => @"^Set\s([Rr][\d]+)\s((\d)|(?>....))$";

        protected override string OpCodeAsm
            => "(Set)";

        protected override byte OpCode
            => 0x40;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                destReg,
                (byte)(val >> 8),
                (byte)val
            };
        }



        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Set();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.destReg = byte.Parse(match.Groups[1].Value.Substring(1));

            if (Regex.Match(match.Groups[2].Value, $"{start}{hexValue}").Success)
            {

                instruction.val = ushort.Parse(match.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                instruction.val = ushort.Parse(match.Groups[2].Value);
            }

            originalAssembly = instruction.originalAssembly;
            destReg = instruction.destReg;
            val = instruction.val;

            return instruction;
        }
    }
}
