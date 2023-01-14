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
            return FindPositionOf<Player>(new ApplicationException("Player not found"));
        }

        private Point FindPositionOf<T>()
        {
            return FindPositionOf<T>(new InvalidOperationException("Not found"));
        }

        private Point FindPositionOf<T>(T obj) where T : IFieldEntity
        {
            return FindPositionOf<T>(new InvalidOperationException("Not found"), obj);
        }

        private Point FindPositionOf<T>(Exception ex)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (Field[y, x] is T)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw ex;
        }
        private Point FindPositionOf<T>(Exception ex, T obj) where T : IFieldEntity
        {
            IFieldEntity item = obj;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (Field[y, x] == item)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw ex;
        }

        private bool IsBoardFull()
        {
            try
            {
                FindPositionOf<IFieldEntity>(default(IFieldEntity));
                return false;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }

        public void SpawnEntity(IFieldEntity entity)
        {
            if (IsBoardFull())
            {
                return;
            }

            Point position = GenerateRandomPosition();

            SpawnEntityAt(entity, position);
        }

        public void SpawnEntityAt(IFieldEntity entity, Point position)
        {
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

            if (Field[position.Y, position.X] is not null
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
