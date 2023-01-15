using grab_the_cheese.enums;

namespace grab_the_cheese.game
{
    internal class GameConfig
    {
        public BoardSize BoardSize { get; set; }

        public GameConfig(BoardSize boardSize)
        {
            BoardSize = boardSize;
        }
    }
}
