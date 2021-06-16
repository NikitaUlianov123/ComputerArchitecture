using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions.Flow_Control
{
    public class GoToLabels : Instruction
    {
        private string originalAssembly;
        public string Label;

        protected override string Pattern
            => @"^Goto +(\w+) *(?:\/\/.*)*"; //$"{start}{OpCodeAsm}{space}{label}{comments}$";

        protected override string OpCodeAsm
            => "(Goto)";

        protected override byte OpCode
            => 0x32;

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

            var instruction = new GoToLabels();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.Label = Convert.ToString(match.Groups[1].Value);

            Label = instruction.Label;

            return instruction;
        }

        public override bool IsGoToLabels()
        {
            return true;
        }
    }
}
