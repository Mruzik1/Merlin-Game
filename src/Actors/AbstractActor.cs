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

            if (posX+animation.GetWidth() > otherX && otherX+otherW > posX &&
                posY+animation.GetHeight() > otherY && otherY+otherH > posY)
                return true;
                
            return false;
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

        public bool RemovedFromWorld() => toBeRemoved;

        public abstract void Update();
    }
}