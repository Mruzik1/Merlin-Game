namespace MyGame.Commands
{
    public class HealthOverTime : HealthEffect
    {

        private double stepDuration;
        private double time;
        private int steps;

        // stepDuration should be in ms
        public HealthOverTime(string name) : base(name)
        {
            this.time = 0;
        }

        public void SetDuration(double duration)
        {
            stepDuration = (duration / 1000) * 60;
        }

        public void SetSteps(int steps)
        {
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