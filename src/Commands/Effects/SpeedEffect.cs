using MyGame.Actors;


namespace MyGame.Commands
{
    public class SpeedEffect: AbstractEffect
    {
        private ISpeedStrategy speedStrategy;
        private double duration;
        private double time;

        public SpeedEffect(ISpeedStrategy newSpeedStrategy, double duration)
        {
            this.speedStrategy = newSpeedStrategy;
            this.duration = (duration / 1000) * 60;
            this.time = 0;
        }

        public override void Execute()
        {
            if (target != null)
                target.SetSpeedStrategy(speedStrategy);

            if (time >= duration)
            {
                target.SetSpeedStrategy(new NormalSpeedStrategy());
            }
            
            time++;
        }
    }
}