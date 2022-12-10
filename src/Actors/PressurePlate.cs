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
            animationPressed = new Animation("resources/sprites/pressed_plate.png", 40, 4);
            animationDefault = new Animation("resources/sprites/released_plate.png", 40, 8);
            
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
                intersector = null;
                mechanism.Deactivate();
                Release();
            }

            GetAnimation().Start();
        }

        public void SetMechanism(IMechanism mechanism)
        {
            this.mechanism = mechanism;
        }

        public void Press()
        {
            SetAnimation(animationPressed);
        }

        public void Release()
        {
            SetAnimation(animationDefault);
        }

        public override void Update()
        {
            Use(intersector);
            
            GetWorld().GetActors().ForEach(actor => {
                if (intersector == null && 
                    actor is ICharacter &&
                    IntersectsWithActor(actor))
                {
                    intersector = actor;
                }
            });
        }
    }
}