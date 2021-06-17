using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary.Instructions
{
    public abstract class Instruction
    {
        protected byte padding = 0xFF;

        protected string start = @"^";
        protected string end = @"$";
        protected string register = @"R([012]\d|3[01]|\d)";
        protected string space = @" +";
        protected string comments = @"(?:\/\/)";
        protected string literalValue = @"(\d*)";
        protected string hexValue = @"([[:xdigit:]]+)";
        protected string label = @"(\w+)";

        protected abstract string Pattern { get; }
        protected abstract string OpCodeAsm { get; }
        protected abstract byte OpCode { get; }

        public abstract Instruction Parse(string assembly);
        public abstract byte[] Emit();

        public virtual bool IsGoToLabels()
        {
            return false;
        }

        public virtual bool IsBreqLabel()
        {
            return false;
        }
    }
}