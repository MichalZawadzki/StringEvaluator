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
            ICollection<string> factors = SplitExpressionIntoFactors(expression);
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

        public ICollection<string> SplitExpressionIntoFactors(string expression)
        {
            Regex regex = new Regex(@"(\d+|[-+\/*]){1}");
            return regex.Matches(expression).Select(match => match.Value).ToList();
        }
    }
}
