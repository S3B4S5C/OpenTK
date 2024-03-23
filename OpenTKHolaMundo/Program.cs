// See https://aka.ms/new-console-template for more information
using System;
using OpenTK;

using OpenTKHolaMundo;
namespace OpenTKHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Game game = new Game())
            {
                game.Run();
            }
        }
    }
}

