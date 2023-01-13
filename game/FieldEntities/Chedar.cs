using grab_the_cheese.interfaces;
using System;

namespace grab_the_cheese.game.FieldEntities
{
    internal class Chedar : Collectable, IFieldEntity
    {
        public Chedar()
        {
            Points = 25;
        }

        public void PrintDisplayValue()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("C");
            Console.ResetColor();
        }
    }
}
