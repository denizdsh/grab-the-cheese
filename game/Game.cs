using grab_the_cheese.enums;
using grab_the_cheese.game.FieldEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace grab_the_cheese.game
{
    internal class Game
    {
        public Board Board { get; private set; }
        public GameConfig Config { get; private set; }
        public double Score { get; private set; }

        public Game(Board board, GameConfig config)
        {
            Board = board;
            Config = config;
        }

        public void StartGame()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                string inputKey = Console.ReadKey(true).Key.ToString();
                MovementKey key;

                if (!Enum.TryParse<MovementKey>(inputKey, out key))
                {
                    continue;
                }

                Move(key);
            }
        }

        private void Move(object movement)
        {
            Console.WriteLine(movement);
        }
    }
}
