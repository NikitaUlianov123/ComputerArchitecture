using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Memory
{
    public class Loadi : Instruction
    {
        private string originalAssembly;
        private byte sourceReg;
        private byte destReg;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{register}{space}{register}";

        protected override string OpCodeAsm
            => "(Loadi)";

        protected override byte OpCode
            => 0x46;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                sourceReg,
                destReg,
                padding
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Loadi();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.sourceReg = byte.Parse(match.Groups[2].Value);
            instruction.destReg = byte.Parse(match.Groups[3].Value);

            originalAssembly = instruction.originalAssembly;
            sourceReg = instruction.sourceReg;
            destReg = instruction.destReg;

            return instruction;
        }
    }
}
