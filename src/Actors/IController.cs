namespace MyGame.Actors
{
    public interface IController : IUsable
    {
        void SetMechanism(IMechanism mechanism);
    }
}