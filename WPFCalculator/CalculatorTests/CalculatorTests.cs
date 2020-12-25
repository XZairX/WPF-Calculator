using CalculatorApp;
using Moq;
using System;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsAPlusB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Add(a, b);

            Assert.Equal(a + b, result);
        }

        [Fact]
        public void Subtract_ReturnsAMinusB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Subtract(a, b);

            Assert.Equal(a - b, result);
        }

        [Fact]
        public void Multiply_ReturnsAMultipliedByB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Multiply(a, b);

            Assert.Equal(a * b, result);
        }

        [Fact]
        public void Divide_BIsZero_ThrowsDivideByZeroException()
        {
            double a = It.IsAny<int>();
            double b = 0;

            Assert.Throws<DivideByZeroException>(() => Calculator.Divide(a, b));
            //Exception.TypeOf<DivideByZeroException>()
            //.With.Message.EqualTo("Cannot divide by zero"))
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        public void Divide_BIsNotZero_ReturnsADividedByB(int b)
        {
            double a = It.IsAny<int>();

            var result = Calculator.Divide(a, b);

            Assert.Equal(a / b, result);
        }

        [Fact]
        public void Modulo_BIsZero_ThrowsDivideByZeroException()
        {
            double a = It.IsAny<int>();
            double b = 0;

            Assert.Throws<DivideByZeroException>(() => Calculator.Modulo(a, b));
                //Throws.Exception.TypeOf<DivideByZeroException>()
                //.With.Message.EqualTo("Cannot divide by zero"));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        public void Modulo_BIsNotZero_ReturnsAModuloB(int b)
        {
            double a = It.IsAny<int>();

            var result = Calculator.Modulo(a, b);

            Assert.Equal(a % b, result);
        }
    }
}
