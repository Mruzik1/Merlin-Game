using MyGame.Actors;


namespace MyGame.Commands
{
    public class SpeedEffect: AbstractEffect
    {
        private ISpeedStrategy speedStrategy;
        private double duration;
        private double time;

        public SpeedEffect(string name, ISpeedStrategy newSpeedStrategy) : base(name)
        {
            this.speedStrategy = newSpeedStrategy;
            this.time = 0;
        }

        public void SetDuration(double duration)
        {
            this.duration = (duration / 1000) * 60;
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