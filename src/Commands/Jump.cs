using MyGame.Actors;


namespace MyGame.Commands
{
    public class Jump : Move
    {
        private int jumpHeight;
        private bool jumping;
        private int startingY;
        private ISpeedStrategy jumpSpeedStrategy;

        public Jump(int jumpHeight, IMovable jumper) : base(jumper, 0, 0)
        {
            this.jumpHeight = jumpHeight;
            this.jumping = false;
        }

        public override void Execute()
        {
            int oldY = actor.GetY();

            ((IMovable)actor).SetSpeedStrategy(jumpSpeedStrategy);
            base.Execute();
            ((IMovable)actor).SetSpeedStrategy(new NormalSpeedStrategy());

            if (startingY-actor.GetY() >= jumpHeight || oldY-actor.GetY() == 0)
            {
                jumping = false;
                dy = 0;
                jumpSpeedStrategy = null;
            }
        }

        public void performJump(int startingY)
        {
            jumping = true;
            dy = -1;
            jumpSpeedStrategy = new ModifiedSpeedStrategy(6.5, 0.97);

            this.startingY = startingY;
        }

        public bool IsJumping() => jumping;
    }
}