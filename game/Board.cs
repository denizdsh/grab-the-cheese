using grab_the_cheese.enums;
using grab_the_cheese.game.FieldEntities;
using grab_the_cheese.interfaces;
using System;
using System.Drawing;

namespace grab_the_cheese.game
{
    internal class Board
    {
        public IFieldEntity[,] Field { get; set; }

        private int Size
        {
            get
            {
                return Field.GetLength(0);
            }
        }

        public Board(BoardSize boardSize)
        {
            int size = (int)boardSize;
            Field = new IFieldEntity[size, size];
        }

        public Point FindPlayerPosition()
        {

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (Field[y, x] is Player)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new InvalidOperationException("Player not found");
        }

        public void SpawnEntity(IFieldEntity entity)
        {
            Point position = GenerateRandomPosition();

            Field[position.Y, position.X] = entity;
        }

        public void MakePlayerMove(Point newPosition, Point oldPosition)
        {
            EnsurePosition(newPosition, true);

            IFieldEntity tempPlayer = Field[oldPosition.Y, oldPosition.X];

            Field[oldPosition.Y, oldPosition.X] = null;

            Field[newPosition.Y, newPosition.X] = tempPlayer;
        }

        private void EnsurePosition(Point position, bool isPlayer = false)
        {
            if (position.X < 0 || position.X > Size
                || position.Y < 0 || position.Y > Size)
            {
                throw new ArgumentException("Position outside of board");
            }

            if (Field[position.X, position.Y] is not null
                && !isPlayer)
            {
                throw new ArgumentException($"There is already an item on position {{X: {position.X}; Y: {position.Y} }}");
            }
        }

        private Point GenerateRandomPosition()
        {
            Random random = new Random();

            while (true)
            {
                Point position = new Point(
                    random.Next(Size),
                    random.Next(Size));

                try
                {
                    EnsurePosition(position);
                    return position;
                }
                catch { }
            }
        }
    }
}
