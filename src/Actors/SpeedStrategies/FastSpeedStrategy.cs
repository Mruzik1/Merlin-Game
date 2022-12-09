namespace MyGame.Actors
{
    public class FastSpeedStrategy : ISpeedStrategy
    {
        public double GetSpeed(double speed) => speed*1.5;
    }
}