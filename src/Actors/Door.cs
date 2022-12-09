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

        public void MakeSolid()
        {
            for (int i = 0; i < GetWidth()/16; ++i)
                for (int j = 0; j < GetHeight()/16; ++j)
                {
                    int x = GetX()/GetWorld().GetTileWidth()+i;
                    int y = GetY()/GetWorld().GetTileHeight()+j;

                    GetWorld().SetWall(x, y, !isOpen);
                }
        }

        public void Activate()
        {
            isOpen = true;
            SetAnimation(animationOpen);

            GetAnimation().Start();
            MakeSolid();
        }

        public void Deactivate()
        {
            isOpen = false;
            SetAnimation(animationClosed);

            GetAnimation().Start();
            MakeSolid();
        }

        public override void Update() {}
    }
}