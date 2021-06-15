using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Memory
{
    public class Push : Instruction
    {
        private string originalAssembly;
        private byte sourceReg;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{register}{space}{comments}$";

        protected override string OpCodeAsm
            => "(Push)";

        protected override byte OpCode
            => 0x41;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                sourceReg,
                padding,
                padding
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Push();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.sourceReg = byte.Parse(match.Groups[2].Value);

            return instruction;
        }
    }
}
