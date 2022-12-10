using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public abstract class AbstractActor : IActor
    {
        private int posX;
        private int posY;
        private string name;
        private bool toBeRemoved;
        private IWorld world;
        protected Animation animation;
        private bool affectedByPhysics;
        

        public AbstractActor(string name)
        {
            this.name = name;
        }

        public AbstractActor() : this("")
        {
            toBeRemoved = false;
        }
        
        public string GetName() => name;

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetY() => posY;

        public int GetX() => posX;

        public int GetHeight() => animation.GetHeight();

        public int GetWidth() => animation.GetWidth();

        public void SetPosition(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        public void OnAddedToWorld(IWorld world)
        {
            this.world = world;
        }

        public IWorld GetWorld() => world;

        public Animation GetAnimation() => animation;

        public void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }

        public bool IntersectsWithActor(IActor other)
        {
            int otherX = other.GetX();
            int otherY = other.GetY();
            int otherW = other.GetWidth();
            int otherH = other.GetHeight();

            return posX+GetWidth() > otherX && otherX+otherW > posX &&
                    posY+GetHeight() > otherY && otherY+otherH > posY;
        }

        public void SetPhysics(bool isPhysicsEnabled)
        {
            this.affectedByPhysics = isPhysicsEnabled;
        }

        public bool IsAffectedByPhysics() => affectedByPhysics;

        public void RemoveFromWorld()
        {
            toBeRemoved = true;
        }

        public void Renew()
        {
            toBeRemoved = false;
        }

        public bool RemovedFromWorld() => toBeRemoved;

        // makes an actor solid
        public void MakeSolid(bool solid)
        {
            for (int i = 0; i < (int)Math.Ceiling((double)GetWidth()/16); ++i)
                for (int j = 0; j < GetHeight()/16; ++j)
                {
                    int x = (int)Math.Ceiling((double)GetX()/GetWorld().GetTileWidth())+i;
                    int y = (int)Math.Ceiling((double)GetY()/GetWorld().GetTileHeight())+j;

                    GetWorld().SetWall(x, y, solid);
                }
        }

        public abstract void Update();
    }
}