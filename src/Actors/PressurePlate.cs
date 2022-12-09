using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public class PressurePlate : AbstractActor, IUsable
    {
        private IMechanism mechanism;
        private IActor intersector;
        private Animation animationPressed;
        private Animation animationDefault;

        public PressurePlate(int x, int y)
        {
            animationPressed = new Animation("resources/sprites/source_on.png", 56, 60);
            animationDefault = new Animation("resources/sprites/source_off.png", 56, 60);
            
            SetAnimation(animationDefault);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void Use(IActor actor)
        {   
            if (actor == null)
                return;

            if (IntersectsWithActor(actor))
            {
                mechanism.Activate();
                Press();
            }
            else
            {
                actor = null;
                mechanism.Deactivate();
                Release();
            }
        }

        public void SetMechanism(IMechanism mechanism)
        {
            this.mechanism = mechanism;
        }

        public void Press()
        {
            SetAnimation(animationPressed);
            GetAnimation().Start();
        }

        public void Release()
        {
            SetAnimation(animationDefault);
            GetAnimation().Start();
        }

        public override void Update()
        {
            Use(intersector);
            
            GetWorld().GetActors().ForEach(actor => {
                if (intersector == null && (actor as ICharacter) != null && IntersectsWithActor(actor))
                    intersector = actor;
            });
        }
    }
}