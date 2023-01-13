using grab_the_cheese.interfaces;
using System;

namespace grab_the_cheese.game.FieldEntities
{
    internal class Poop : IFieldEntity, IEnemyEntity
    {
        public void PrintDisplayValue()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write('P');
            Console.ResetColor();
        }
    }
}
