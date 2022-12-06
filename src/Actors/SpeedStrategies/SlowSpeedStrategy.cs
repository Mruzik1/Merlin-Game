namespace MyGame.Actors
{
    public class SlowSpeedStrtegy : ISpeedStrategy
    {
        public double GetSpeed(double speed) => speed*0.1;
    }
}