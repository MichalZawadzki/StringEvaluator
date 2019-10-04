using System;

namespace StringEvaluator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******");
            Console.WriteLine("Please put expression consisting of non-negative integers and the [+ - / *] operators only, then press enter:");
            Console.WriteLine("******");

            Console.WriteLine();
            Console.Write("Expression: ");
            string expression = Console.ReadLine();

            try
            {
                BasicEvaluator basicEvaluator = new BasicEvaluator();
                decimal result = basicEvaluator.Evaluate(expression);

                Console.WriteLine();
                Console.WriteLine($"Result: {result}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
