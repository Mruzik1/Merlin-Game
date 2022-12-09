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
            animationOpen = new Animation("resources/sprites/door_open.png", 30, 70);
            animationClosed = new Animation("resources/sprites/door_closed.png", 10, 70);
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
            MakeSolid(false);
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