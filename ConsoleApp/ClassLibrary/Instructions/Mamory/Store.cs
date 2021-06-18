using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Mamory
{
    class Store : Instruction
    {
        private string originalAssembly;
        private byte sourceReg;
        private ushort memAddress;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{register}{space}{literalValue}{comments}$";

        protected override string OpCodeAsm
            => "(Store)";

        protected override byte OpCode
            => 0x43;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                sourceReg,
                (byte)(memAddress >> 8),
                (byte)memAddress
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Store();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.sourceReg = byte.Parse(match.Groups[2].Value);
            instruction.memAddress = ushort.Parse(match.Groups[3].Value);

            return instruction;
        }
    }
}
