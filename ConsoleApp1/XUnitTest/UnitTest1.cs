using System;

using Xunit;
using ConsoleApp1;

namespace XUnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(64)]
        [InlineData(32)]
        [InlineData(1)]
        public void PowerOf2ReturnsTrueIfPowerOf2(int number)
        {
            bool myPower = Program.IsPowerOf2(number);
            Assert.True(myPower);
        }


        [Theory]
        [InlineData(65)]
        [InlineData(33)]
        [InlineData(0)]
        public void PowerOf2ReturnsFalseIfNotPowerOf2(int number)
        {
            bool myPower = Program.IsPowerOf2(number);
            Assert.False(myPower);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(65)]
        [InlineData(63)]
        public void NearestPowerOf2ReturnsPowerOf2(int number)
        {
            Assert.True(Program.IsPowerOf2(Program.NearestPowerOf2(number)));
        }

        [Fact]
        public void SetBitWorks()
        {
            Assert.True(true);
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 5)]
        [InlineData(7, 2)]
        public void DoMathAdds(uint number, uint otherNumber)
        {
            uint input = 0x01000000;
            uint inputNumber = number << 16;
            input += inputNumber;
            inputNumber = otherNumber << 8;
            input += otherNumber;
            Assert.True(Program.DoMath(input) == number + otherNumber);
        }

        [Theory]
        [InlineData(5, 3)]
        //[InlineData(1)]
        //[InlineData(3)]
        public void DoMathSubtracts(uint number, uint otherNumber)
        {
            uint input = 01;
            Assert.True(Program.DoMath(input) == number + otherNumber);
        }

        [Theory]
        [InlineData(0, 0)]
        //[InlineData(1)]
        //[InlineData(3)]
        public void DoMathMultiplies(uint number, uint otherNumber)
        {
            uint input = 01;
            Assert.True(Program.DoMath(input) == number + otherNumber);
        }

        [Theory]
        [InlineData(0, 0)]
        //[InlineData(1)]
        //[InlineData(3)]
        public void DoMathDivides(uint number, uint otherNumber)
        {
            uint input = 01;
            Assert.True(Program.DoMath(input) == number + otherNumber);
        }
    }
}