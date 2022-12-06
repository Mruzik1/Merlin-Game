namespace MyGame.Commands
{
    public class HealthOverTime : HealthEffect
    {

        private double stepDuration;
        private double time;
        private int steps;

        // stepDuration should be in ms
        public HealthOverTime(int hp, int steps, double stepDuration) : base(hp)
        {
            this.stepDuration = (stepDuration / 1000) * 60;
            this.time = 0;
            this.steps = steps;
        }

        public override void Execute()
        {
            if (time >= stepDuration)
            {   
                if (target != null)
                    target.ChangeHealth(hp);
                time = 0;
                steps--;
            }
            else
                time++;
            
            if (steps == 0)
                RemoveTarget();
        }
    }
}