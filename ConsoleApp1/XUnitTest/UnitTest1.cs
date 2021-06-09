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
    }
}
