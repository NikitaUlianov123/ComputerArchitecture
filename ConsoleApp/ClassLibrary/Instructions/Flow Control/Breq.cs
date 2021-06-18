using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Flow_Control
{
    public class Breq : Instruction
    {
        private string originalAssembly;
        public byte checkReg;
        public ushort memaddress;

        protected override string Pattern
            => $@"{start}{OpCodeAsm}{space}{register}{space}{hexValue}"; //{start}{OpCodeAsm}{space}{register}{space}{literalValue} //{comments}

        protected override string OpCodeAsm
            => "(BREQ)";

        protected override byte OpCode
            => 0x35;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                checkReg,
                (byte)(memaddress >> 8),
                (byte)memaddress
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Breq();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.checkReg = byte.Parse(match.Groups[2].Value);
            instruction.memaddress = ushort.Parse(match.Groups[3].Value, System.Globalization.NumberStyles.HexNumber);

            originalAssembly = instruction.originalAssembly;
            checkReg = instruction.checkReg;
            memaddress = instruction.memaddress;

            return instruction;
        }
    }
}
