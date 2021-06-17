using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Flow_Control
{
    public class GoToi : Instruction
    {
        private string originalAssembly;
        private byte destReg;

        protected override string Pattern
            => $@"{start}{OpCodeAsm}{space}{register}"; //{start}{OpCodeAsm}{space}{register}{space}{comments}";

        protected override string OpCodeAsm
            => "(Gotoi)";

        protected override byte OpCode
            => 0x31;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                destReg,
                padding,
                padding
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new GoToi();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.destReg = byte.Parse(match.Groups[2].Value);

            originalAssembly = instruction.originalAssembly;
            destReg = instruction.destReg;

            return instruction;
        }
    }
}
