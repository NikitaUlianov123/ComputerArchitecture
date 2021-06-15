using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Flow_Control
{
    public class Nop : Instruction
    {
        private string originalAssembly;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{comments}$";

        protected override string OpCodeAsm
            => "(Nop)";

        protected override byte OpCode
            => 0x00;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                padding,
                padding,
                padding
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Nop();

            instruction.originalAssembly = match.Groups[0].Value;

            return instruction;
        }
    }
}
