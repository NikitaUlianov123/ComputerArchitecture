using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Logic
{
    public class Or : Instruction
    {
        private string originalAssembly;
        private byte Reg1;
        private byte Reg2;
        private byte destReg;

        protected override string Pattern
            => $"{start}{OpCodeAsm}{space}{register}{space}{register}{space}{register}";

        protected override string OpCodeAsm
            => "(Or)";

        protected override byte OpCode
            => 0x23;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                Reg1,
                Reg2,
                destReg
            };
        }

        public override Instruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (!match.Success) return null;

            var instruction = new Or();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.Reg1 = byte.Parse(match.Groups[2].Value);
            instruction.Reg2 = byte.Parse(match.Groups[3].Value);
            instruction.destReg = byte.Parse(match.Groups[4].Value);

            originalAssembly = instruction.originalAssembly;
            Reg1 = instruction.Reg1;
            Reg2 = instruction.Reg2;
            destReg = instruction.destReg;

            return instruction;
        }
    }
}
