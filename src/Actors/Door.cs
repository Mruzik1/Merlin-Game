using Merlin2d.Game;


namespace MyGame.Actors
{
    public class Door : AbstractActor, IMechanism
    {
        private Animation animationOpen;
        private Animation animationClosed;
        private bool isOpen;

        public Door(int x, int y)
        {
            animationOpen = new Animation("resources/sprites/crystal_off.png", 56, 56);
            animationClosed = new Animation("resources/sprites/crystal_on.png", 56, 56);
            isOpen = false;
            
            SetPosition(x, y);
            SetAnimation(animationClosed);
            GetAnimation().Start();
        }

        public bool IsActivated()
        {
            return isOpen;
        }

        public void Activate()
        {
            isOpen = true;
            SetAnimation(animationOpen);

            GetAnimation().Start();
            // MakeSolid(false);
        }

        public void Deactivate()
        {
            isOpen = false;
            SetAnimation(animationClosed);

            GetAnimation().Start();
            MakeSolid(true);
        }

        public override void Update() {}
    }
}