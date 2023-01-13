using grab_the_cheese.enums;
using grab_the_cheese.interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grab_the_cheese.game
{
    internal class Board
    {
        public IFieldEntity[,] Field { get; set; }

        public Board(BoardSize boardSize)
        {
            int size = (int)boardSize;
            Field = new IFieldEntity[size, size];
        }

        public void UpdatePlayerPosition()
        {
            
        }

        public Point FindPlayerPosition()
        {
            return Point.Empty;
        }
    }
}
