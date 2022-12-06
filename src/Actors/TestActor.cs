using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public class TestActor : AbstractActor
    {
        private IActor player;
        private Animation animationOn;
        private Animation animationOff;

        public TestActor(int x, int y)
        {
            animationOff = new Animation("resources/sprites/crystal_off.png", 56, 56);
            animationOn = new Animation("resources/sprites/crystal_on.png", 56, 56);
            
            SetAnimation(animationOff);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void SetPlayer(IActor player)
        {
            this.player = player;
        }

        public override void Update()
        {
            if (IntersectsWithActor(player))
                SetAnimation(animationOn);
            else
                SetAnimation(animationOff);
        }
    }
}