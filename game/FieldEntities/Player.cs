using grab_the_cheese.interfaces;
using System;

namespace grab_the_cheese.game.FieldEntities
{
    internal class Player : IFieldEntity
    {
        public void PrintDisplayValue()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("M");
            Console.ResetColor();
        }
    }
}
