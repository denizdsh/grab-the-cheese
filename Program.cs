using grab_the_cheese.game;
using grab_the_cheese.enums;
using System;

namespace grab_the_cheese
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.ConfigureGame();
            game.StartGame();
        }
    }
}
