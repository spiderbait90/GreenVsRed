using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenVsRed
{
    class App
    {
        static void Main()
        {
            var helper = new InputOutput();
            var game = new Game(helper);

            game.Start();
        }
    }
}
