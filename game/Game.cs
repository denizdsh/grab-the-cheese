using grab_the_cheese.enums;
using grab_the_cheese.game.FieldEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
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

        public MenuConfig Menu { get; private set; }

        public Game()
        {
            Menu = new MenuConfig();
        }

        public void ConfigureGame()
        {
            // Start menu
            Menu.PrintMenuConfig();

            while (true)
            {
                string key = Console.ReadKey(true).Key.ToString();

                if (key == "Enter")
                {
                    break;
                }
            }

            Console.Clear();


            // Difficulty menu
            Menu.PrintGameChoiceMenu();

            Difficulty difficulty;
            while (true)
            {
                string key = Console.ReadKey(true).Key.ToString()[1].ToString();

                if (Enum.TryParse<Difficulty>(key, out difficulty))
                {
                    break;
                }
            }
            Console.Clear();

            // Configure and start game
            GameConfig config = new GameConfig(BoardSize.MasterSplinterHideout, difficulty);
            Board = new Board(config.BoardSize);

            StartGame();
        }

        public void StartGame()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Board.SpawnEntity(new Player());
            Board.SpawnEntity(new Chedar());

            Menu.PrintGameBoard(Board);

            while (true)
            {
                string inputKey = Console.ReadKey(true).Key.ToString();
                MovementKey key;

                if (!Enum.TryParse<MovementKey>(inputKey, out key))
                {
                    continue;
                }

                try
                {
                    UpdatePlayerPosition(key);
                }
                catch { continue; }

                Console.Clear();

                Menu.PrintGameBoard(Board);
            }
        }

        public void UpdatePlayerPosition(MovementKey key)
        {
            Point currentPos = Board.FindPlayerPosition();

            Point newPos;

            switch (key)
            {
                case MovementKey.UpArrow:
                    newPos = new Point(currentPos.X, currentPos.Y - 1);
                    break;

                case MovementKey.DownArrow:
                    newPos = new Point(currentPos.X, currentPos.Y + 1);
                    break;

                case MovementKey.RightArrow:
                    newPos = new Point(currentPos.X + 1, currentPos.Y);
                    break;

                case MovementKey.LeftArrow:
                    newPos = new Point(currentPos.X - 1, currentPos.Y);
                    break;

                default:
                    throw new ArgumentException("Invalid key");
            }

            if (Board.Field[newPos.Y, newPos.X] is Collectable collectable)
            {
                UpdateScore(collectable.Points);
            }

            Board.MakePlayerMove(newPos, currentPos);
        }

        private void UpdateScore(int points)
        {
            Score += points;
        }
    }
}
