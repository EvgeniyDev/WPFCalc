using System;
using WPFCalc.Services;
using WPFCalc.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTestProject
{
    [TestClass]
    public class CalculatorOperationsTest
    {
        readonly ButtonValues _testButtonValues = new ButtonValues();
        readonly Calculator _testCalculator = new Calculator();

        [TestMethod]
        public void TestingInverseIfInputEquals66()
        {
            string operation = _testButtonValues.INVERSE;
            string calculationInput = "66";
            string calculationHistory = string.Empty;
            (string expectedInput, string expectedHistory) = ("0.015152", "1 / " + calculationInput);

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, operation);

            Assert.AreEqual(expectedInput, actualInput);
            Assert.AreEqual(expectedHistory, actualHistory);
        }

        [TestMethod]
        public void TestingInverseIfInputEquals0()
        {
            string operation = _testButtonValues.INVERSE;
            string calculationInput = "0";
            string calculationHistory = string.Empty;
            (string expectedInput, string expectedHistory) = (double.PositiveInfinity.ToString(), "1 / " + calculationInput);

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, operation);

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingСosIfInputEquals0()
        {
            string operation = _testButtonValues.COS;
            string calculationInput = "0";
            string calculationHistory = string.Empty;
            (string expectedInput, string expectedHistory) = (Math.Cos(0).ToString(), $"cos ({calculationInput})" );

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, operation);

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingСosIfInputEquals0dot041()
        {
            string operation = _testButtonValues.COS;
            string calculationInput = "0,041";
            string calculationHistory = string.Empty;
            (string expectedInput, string expectedHistory) = ("0.99916", $"cos ({calculationInput})");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, operation);

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingSQRTIfInputEquals4()
        {
            string operation = _testButtonValues.SQRT;
            string calculationInput = "4";
            string calculationHistory = string.Empty;
            (string expectedInput, string expectedHistory) = (Math.Sqrt(4).ToString(), $"√{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, operation);

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingSQRTIfInputEqualsMinus9()
        {
            string operation = _testButtonValues.SQRT;
            string calculationInput = "-9";
            string calculationHistory = string.Empty;
            (string expectedInput, string expectedHistory) = (Math.Sqrt(-9).ToString(), $"√{calculationInput}");

            (string actualInput, string actualHistory) = 
                _testCalculator.Calculate(calculationInput, calculationHistory, operation);

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingPLUSfInputEquals9firstArgumentEquals81()
        {
            string operation = _testButtonValues.PLUS;
            _testCalculator.firstArgument = 81;
            string calculationInput = "9";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory) = ((81 + 9).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingPLUSfInputEqualsMinus50firstArgumentEquals100()
        {
            string operation = _testButtonValues.PLUS;
            _testCalculator.firstArgument = 100;
            string calculationInput = "-50";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory) = ((-50 + 100).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingMINUSfInputEquals199firstArgumentEquals237()
        {
            string operation = _testButtonValues.MINUS;
            _testCalculator.firstArgument = 237;
            string calculationInput = "199";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory)
                = ((237 - 199).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingMINUSfInputEqualsMinus3firstArgumentEqualsMinus34()
        {
            string operation = _testButtonValues.MINUS;
            _testCalculator.firstArgument = -34;
            string calculationInput = "-3";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory)
                = ((-34 - (-3)).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingMULTIPLYfInputEquals0FirstArgumentEquals71()
        {
            string operation = _testButtonValues.MULTIPY;
            _testCalculator.firstArgument = 71;
            string calculationInput = "0";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory)
                = ((71 * 0).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingMULTIPLYfInputEquals171FirstArgumentEqualsMinus34()
        {
            string operation = _testButtonValues.MULTIPY;
            _testCalculator.firstArgument = -34;
            string calculationInput = "171";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory)
                = ((-34 * 171).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingDIVIDEfInputEqualsMinus3FirstArgumentEquals21()
        {
            string operation = _testButtonValues.DIVIDE;
            _testCalculator.firstArgument = 21;
            string calculationInput = "-3";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory)
                = ((21 / -3).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }

        [TestMethod]
        public void TestingDIVIDEfInputEquals0FirstArgument100()
        {
            string operation = _testButtonValues.DIVIDE;
            _testCalculator.firstArgument = 100;
            string calculationInput = "0";
            string calculationHistory = $"{_testCalculator.firstArgument}{operation}";
            (string expectedInput, string expectedHistory)
                = ((100d / 0).ToString(), $"{calculationHistory}{calculationInput}");

            (string actualInput, string actualHistory) =
                _testCalculator.Calculate(calculationInput, calculationHistory, "=");

            Assert.AreEqual((expectedInput, expectedHistory), (actualInput, actualHistory));
        }
    }
}
