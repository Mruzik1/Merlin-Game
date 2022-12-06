namespace MyGame.Actors
{
    public class NormalSpeedStrategy : ISpeedStrategy
    {
        public double GetSpeed(double speed) => speed;
    }
}