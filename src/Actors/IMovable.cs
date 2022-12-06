namespace MyGame.Actors
{
    public interface IMovable
    {
        public void SetSpeedStrategy(ISpeedStrategy strategy);
        public double GetSpeed();
    }
}