using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenVsRed
{
    class InputOutput
    {
        public void PrintResult(int timesGreen)
        {
            Console.WriteLine($"Result: {timesGreen}");
        }

        public int[] GetInputFromUser()
        {
            var sizeOfGridTokens = Console.ReadLine()
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            return sizeOfGridTokens;
        }

        public void WaitForKeyPress()
        {
            Console.ReadKey();
        }
    }
}
