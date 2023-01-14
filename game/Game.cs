using grab_the_cheese.enums;
using grab_the_cheese.game.FieldEntities;
using grab_the_cheese.interfaces;
using System;
using System.Drawing;

namespace grab_the_cheese.game
{
    internal class Game
    {
        private readonly Type[] collectableTypes = new Type[] {
            typeof(Chedar),
            typeof(Emental),
            typeof(Gorgonzola)
        };

        private Board Board { get; set; }
        private GameConfig Config { get; set; }
        public double Score { get; private set; }
        private MenuConfig Menu { get; set; }
        private JsonFileOperationService<Statistics> StatisticsService { get; set; }

        public Game()
        {
            Menu = new MenuConfig();
            StatisticsService = new JsonFileOperationService<Statistics>(
                "../../../statistics.json",
                () => new Statistics());
        }

        public void ConfigureGame()
        {
            // Start menu
            Menu.PrintMenuConfig(StatisticsService.GetObject());

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
            this.Config = new GameConfig(BoardSize.MasterSplinterHideout, difficulty);
            Board = new Board(Config.BoardSize);
        }

        public void StartGame()
        {
            Board.SpawnEntity(new Player());
            Board.SpawnEntity(new Chedar());

            Menu.PrintGameMessage(Score);
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
                catch (ApplicationException)
                {
                    break;
                }
                catch
                {
                    continue;
                }

                Console.Clear();
                Menu.PrintGameMessage(Score);
                Menu.PrintGameBoard(Board);
            }

            ConfigureGame();
            StartGame();
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

            bool GrabbedTheCheese = false;

            IFieldEntity nextMoveEntity = Board.Field[newPos.Y, newPos.X];

            if (nextMoveEntity is Collectable collectable)
            {
                GrabbedTheCheese = true;
                UpdateScore(collectable.Points);
            }
            else if (nextMoveEntity is Poop)
            {
                EndGame();
            }

            Board.MakePlayerMove(newPos, currentPos);

            if (GrabbedTheCheese)
            {
                Board.SpawnEntityAt(new Poop(), currentPos);

                SpawnRandomCollectables();
            }
        }

        private void SpawnRandomCollectables()
        {
            Random random = new Random();

            int collectablesSpawnsCount = Config.BoardSize >= BoardSize.Large
                ? 2
                : 1;

            for (int i = 1; i <= collectablesSpawnsCount; i++)
            {
                int idx = random.Next(0, this.collectableTypes.Length);

                Board.SpawnEntity((IFieldEntity)Activator.CreateInstance(this.collectableTypes[idx]));
            }
        }

        private void EndGame()
        {
            Console.Clear();
            Menu.PrintEndGameMessage(Score);
            Menu.PrintGameBoard(Board);

            UpdateStatistics();

            ResetGame();

            throw new ApplicationException("You have died");
        }

        private void ResetGame()
        {
            Score = 0;
            Board = null;
            Config = null;
        }

        private void UpdateScore(int points)
        {
            Score += points;
        }

        private void UpdateStatistics()
        {
            Statistics statistics = StatisticsService.GetObject();

            statistics.TotalGames++;
            statistics.Score += Score;

            StatisticsService.UpdateObject(statistics);
        }
    }
}
