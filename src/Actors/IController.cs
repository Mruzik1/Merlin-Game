namespace MyGame.Actors
{
    public interface IController : IUsable
    {
        void SetMechanisms(List<IMechanism> mechanisms);
    }
}