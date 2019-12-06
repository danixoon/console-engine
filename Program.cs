using System;
using ConsoleEngine.Core;

namespace Pong
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                GameEngine.Init(short.Parse(args[0]), short.Parse(args[1]), true);
            }
            else
            {
                GameEngine.Init(120, 50, true);
            }
        }
    }
}
