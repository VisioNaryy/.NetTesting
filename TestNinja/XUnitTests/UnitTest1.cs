using System;
using System.IO;
using Xunit;

namespace XUnitTests
{
    public class UnitTest1
    {
        private string _fullPath;

        public UnitTest1()
        {
            _fullPath = File.ReadAllText(Directory.GetCurrentDirectory() + @"\TestFiles\" + "TextFile1.txt");
        }

        [Theory]
        [InlineData(1)]
        public void Test1(int? x)
        {
            var ex = Record.Exception(() => ThrowEx(x));
            Assert.DoesNotContain("Custom", ex?.Message);
        }

        private void ThrowEx(int? x)
        {
            if (x == null)
                throw new ApplicationException("Custom exception");
        }
    }
}