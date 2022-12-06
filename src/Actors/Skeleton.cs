using Merlin2d.Game;
using MyGame.Commands;


namespace MyGame.Actors
{
    public class Skeleton : AbstractCharacter
    {
        private Random random;

        private int triggeringZone;
        private int[] randomTarget;
        private Player player;

        public Skeleton(int x, int y, double speed, int health, int triggeringZone) : base(speed, health)
        {
            random = new Random();

            this.triggeringZone = triggeringZone;
            randomTarget = new int[] {random.Next(0, 2000), random.Next(0, 400)};
            SetJump(new Jump(175, this));
            
            Animation animation = new Animation("resources/sprites/enemy.png", 33, 47);
            SetAnimation(animation);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }
        
        // chase some target
        public void WalkTo(int x, int y)
        {
            int oldX = GetX();

            // go to the left
            if (GetX() - x > 0)
            {
                ChangeDirection(ActorOrientation.FacingLeft);
                animation.Start();
                moveLeft.Execute();
            }
            // go to the right
            else if (GetX() - x < 0)
            {
                ChangeDirection(ActorOrientation.FacingRight);
                animation.Start();
                moveRight.Execute();
            }
            // x is the same for both
            else
                animation.Stop();
            
            // jump under some conditions
            MakeJump((GetY() - y > 80 && Math.Abs(GetX() - x) < 45) || (oldX == GetX() && Math.Abs(GetX() - x) != 0));
        }

        public override void Walking()
        {

        }

        public override void Update()
        {
            // Execute effects
            for (int i = 0; i < effects.Count; ++i)
                effects[i].Execute();
        }
    }
}