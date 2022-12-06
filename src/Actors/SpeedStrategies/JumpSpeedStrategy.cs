namespace MyGame.Actors
{
    public class JumpSpeedStrategy : ISpeedStrategy
    {
        private double speedCoef;
        private double scalingCoef;

        public JumpSpeedStrategy(double speedCoefficient, double scalingCoefficient)
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