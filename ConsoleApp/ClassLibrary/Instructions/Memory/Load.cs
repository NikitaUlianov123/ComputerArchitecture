using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Memory
{
    public class Load : Instruction
    {
        private string originalAssembly;
        private byte destReg;
        private ushort memAddress;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{register}{space}{literalValue}{comments}$";

        protected override string OpCodeAsm
            => "(Load)";

        protected override byte OpCode
            => 0x44;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                destReg,
                (byte)(memAddress >> 8),
                (byte)memAddress
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Load();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.destReg = byte.Parse(match.Groups[2].Value);
            instruction.memAddress = ushort.Parse(match.Groups[3].Value);

            return instruction;
        }
    }
}
