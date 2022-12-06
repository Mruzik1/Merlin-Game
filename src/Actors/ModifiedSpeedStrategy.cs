namespace MyGame.Actors
{
    public class ModifiedSpeedStrategy : ISpeedStrategy
    {
        private double speedCoef;
        private double scalingCoef;

        public ModifiedSpeedStrategy(double speedCoefficient, double scalingCoefficient)
        {
            speedCoef = speedCoefficient;
            scalingCoef = scalingCoefficient;
        }

        public double GetSpeed(double speed)
        {   
            if (speedCoef >= speed*1.9)
                speedCoef *= scalingCoef;
                
            return 2*speedCoef;
        }
    }
}