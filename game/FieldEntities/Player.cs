using grab_the_cheese.interfaces;

namespace grab_the_cheese.game.FieldEntities
{
    internal class Player : IFieldEntity
    {
        public string GetDisplayValue()
        {
            return "M";
        }
    }
}
