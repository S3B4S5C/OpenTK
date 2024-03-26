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
            Game game = new Game(800, 700, "Hola Mundo");
            game.Run();
        }
    }
}

