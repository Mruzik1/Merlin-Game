namespace MyGame.Actors.Items
{
    public class FullInventoryException : Exception
    {
        public FullInventoryException() : base() {}
        public FullInventoryException(string msg) : base(msg) {}
    }
}