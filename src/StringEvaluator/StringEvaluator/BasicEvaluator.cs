using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringEvaluator
{
    public class BasicEvaluator
    {
        public decimal Evaluate(string expression)
        {
            ValidateAndStandarize(ref expression);
            List<string> mathFactors = GetSplitExpression(expression);
            return this.Evaluate(mathFactors);
        }

        public decimal Evaluate(List<string> mathFactors)
        {
            string[] orderedMathOperators = new[] { "*", "/", "+", "-" };
            while (mathFactors.Count > 1)
            {
                foreach (string mathOperator in orderedMathOperators)
                {
                    int operatorIndex = mathFactors.ToList().IndexOf(mathOperator);
                    if (operatorIndex > 0)
                    {
                        decimal smallResult = DoCalculation(mathFactors[operatorIndex - 1], mathFactors[operatorIndex], mathFactors[operatorIndex + 1]);
                        ReorganizeFactorsCollection(ref mathFactors, operatorIndex, smallResult);
                        break;
                    }
                }
            }
            return decimal.Parse(mathFactors[0]);
        }

        public void ValidateAndStandarize(ref string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException("Expression cannot be null or empty");
            }

            expression = expression.Replace(" ", "");

            Regex regex = new Regex(@"^(\d+[-+\/*])+\d+$");
            if (!regex.IsMatch(expression))
            {
                throw new ArgumentException("Expression is not valid");
            }
        }

        public List<string> GetSplitExpression(string expression)
        {
            Regex regex = new Regex(@"(\d+|[-+\/*]){1}");
            return regex.Matches(expression).Select(match => match.Value).ToList();
        }

        private decimal DoCalculation(string firstFactor, string operation, string sectorFactor)
        {
            decimal firstNumber = decimal.Parse(firstFactor);
            decimal secondNumber = decimal.Parse(sectorFactor);
            switch (operation)
            {
                case "+":
                    return checked(firstNumber + secondNumber);
                case "-":
                    return checked(firstNumber - secondNumber);
                case "*":
                    return checked(firstNumber * secondNumber);
                case "/":
                    return firstNumber / secondNumber;
                default:
                    throw new InvalidOperationException($"Operation {operation} is not supported");
            }
        }

        private void ReorganizeFactorsCollection(ref List<string> mathFactors, int factorIndex, decimal newNumber)
        {
            mathFactors.RemoveAt(factorIndex);
            mathFactors.Insert(factorIndex, newNumber.ToString());
            mathFactors.RemoveAt(factorIndex + 1);
            mathFactors.RemoveAt(factorIndex - 1);
        }
    }
}
