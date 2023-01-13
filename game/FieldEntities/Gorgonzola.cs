using grab_the_cheese.interfaces;
using System;

namespace grab_the_cheese.game.FieldEntities
{
    internal class Gorgonzola : Collectable, IFieldEntity
    {
        public Gorgonzola()
        {
            Points = 100;
        }

        public void PrintDisplayValue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("G");
            Console.ResetColor();
        }
    }
}
