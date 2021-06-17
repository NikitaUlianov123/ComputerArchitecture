using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Flow_Control
{
    public class BreqLabel : Instruction
    {
        private string originalAssembly;
        public byte checkReg;
        public string Label;

        protected override string Pattern
            => @"^Breq R([012]\d|3[01]|\d) +(\w+) *(?:\/\/.*)*";

        protected override string OpCodeAsm
            => "(Breq)";

        protected override byte OpCode
            => 0x36;

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

            var instruction = new BreqLabel();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.checkReg = byte.Parse(match.Groups[1].Value);
            instruction.Label = Convert.ToString(match.Groups[2].Value);

            originalAssembly = instruction.originalAssembly;
            checkReg = instruction.checkReg;
            Label = instruction.Label;

            return instruction;
        }

        public override bool IsBreqLabel()
        {
            return true;
        }
    }
}
