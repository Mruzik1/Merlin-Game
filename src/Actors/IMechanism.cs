namespace MyGame.Actors
{
    public interface IMechanism
    {
        void Activate();
        void Deactivate();
        bool IsActivated();
    }
}