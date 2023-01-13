using grab_the_cheese.game;
using System;

namespace grab_the_cheese
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(default(Board), default(GameConfig));
            game.StartGame();
        }
    }
}
