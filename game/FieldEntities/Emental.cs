using grab_the_cheese.interfaces;
using System;

namespace grab_the_cheese.game.FieldEntities
{
    internal class Emental : Collectable, IFieldEntity
    {
        public Emental()
        {
            Points = 50;
        }

        public void PrintDisplayValue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("E");
            Console.ResetColor();
        }
    }
}
