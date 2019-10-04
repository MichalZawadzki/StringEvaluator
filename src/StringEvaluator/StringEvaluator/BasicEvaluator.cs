using System;
using System.Text.RegularExpressions;

namespace StringEvaluator
{
    public class BasicEvaluator
    {
        public decimal Evaluate(string expression)
        {
            ValidateAndStandarize(ref expression);

            return 0;
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
    }
}
