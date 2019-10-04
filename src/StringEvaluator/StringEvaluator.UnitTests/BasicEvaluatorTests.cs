using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StringEvaluator.UnitTests
{
    public class BasicEvaluatorTests
    {
        [Test]
        public void throws_error_if_string_expression_is_mull()
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            void evaluateDelegate() => stringEvaluator.Evaluate(null as string);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(() => evaluateDelegate());
            Assert.That(ex.Message, Is.EqualTo("Expression cannot be null or empty"));
        }

        [Test]
        public void throws_error_if_string_expression_is_empty()
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            void evaluateDelegate() => stringEvaluator.Evaluate(string.Empty);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(() => evaluateDelegate());
            Assert.That(ex.Message, Is.EqualTo("Expression cannot be null or empty"));
        }

        [TestCase("+1-")]
        [TestCase("ab1g")]
        [TestCase("+2")]
        [TestCase("2/-+2")]
        public void throws_error_if_string_expression_is_not_valid(string expression)
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            void evaluateDelegate() => stringEvaluator.Evaluate(expression);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(() => evaluateDelegate());
            Assert.That(ex.Message, Is.EqualTo("Expression is not valid"));
        }

        [TestCase("1+1", "1+1")]
        [TestCase("     2   +   1", "2+1")]
        [TestCase("     2   + 323434 /  1   ", "2+323434/1")]
        public void standarize_string_expression(string expression, string standarizedExpression)
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            stringEvaluator.ValidateAndStandarize(ref expression);

            // Assert
            Assert.AreEqual(expression, standarizedExpression);
        }

        [TestCase("1+1", new string[] { "1", "+", "1" })]
        [TestCase("21-1", new string[] { "21", "-", "1" })]
        [TestCase("1200-0/3", new string[] { "1200", "-", "0", "/", "3" })]
        [TestCase("1200-0/3*10", new string[] { "1200", "-", "0", "/", "3", "*", "10" })]
        [TestCase("0+1200-0/3*10", new string[] { "0", "+", "1200", "-", "0", "/", "3", "*", "10" })]
        public void get_list_of_factors(string expression, string[] exprectedListOfFactors)
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            ICollection<string> expressionFactors = stringEvaluator.GetSplitExpression(expression);

            // Assert
            Assert.AreEqual(exprectedListOfFactors.Length, expressionFactors.Count);
            Assert.AreEqual(exprectedListOfFactors.ToList(), expressionFactors.ToList());
        }

        [TestCase("1+1", 1 + 1)]
        [TestCase("1-1", 1 - 1)]
        [TestCase("1*1", 1 * 1)]
        [TestCase("1/1", 1 / 1)]
        [TestCase("1-91", 1 - 91)]
        [TestCase("1+2*3-4/3", 1.0 + (2.0 * 3.0) - (4.0 / 3.0))]
        public void get_result_from_string_expression(string expression, decimal expectedResult)
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            decimal result = stringEvaluator.Evaluate(expression);

            // Assert
            Assert.That(expectedResult, Is.EqualTo(result).Within(0.00000001));
        }

        [TestCase("0/0")]
        [TestCase("1/0")]
        [TestCase("2+4-8/0")]
        [TestCase("1+3/0*7")]
        public void throws_error_when_dividing_by_zero(string expression)
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            void evaluateDelegate() => stringEvaluator.Evaluate(expression);

            // Assert
            Exception ex = Assert.Throws<DivideByZeroException>(() => evaluateDelegate());
        }

        [TestCase("79228162514264337593543950335+10")]
          public void throws_error_when_number_is_too_big(string expression)
        {
            // Arrange
            BasicEvaluator stringEvaluator = new BasicEvaluator();

            // Act
            void evaluateDelegate() => stringEvaluator.Evaluate(expression);

            // Assert
            Exception ex = Assert.Throws<OverflowException>(() => evaluateDelegate());
        }
    }
}