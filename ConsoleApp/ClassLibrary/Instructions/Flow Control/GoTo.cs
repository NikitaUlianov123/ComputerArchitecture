using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Flow_Control
{
    public class GoTo : Instruction
    {
        private string originalAssembly;
        public ushort memAddress;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{hexValue}";

        protected override string OpCodeAsm
            => "(GoTo)";

        protected override byte OpCode
            => 0x30;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                (byte)(memAddress >> 8),
                (byte)memAddress,
                padding
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new GoTo();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.memAddress = ushort.Parse(match.Groups[2].Value);

            originalAssembly = instruction.originalAssembly;
            memAddress = instruction.memAddress;

            return instruction;
        }
    }
}
