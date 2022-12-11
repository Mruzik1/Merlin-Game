using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public class PressurePlate : AbstractActor, IController
    {
        private IEnumerable<IMechanism> mechanisms;
        private IActor intersector;
        private Animation animationPressed;
        private Animation animationDefault;

        public PressurePlate(int x, int y)
        {
            animationPressed = new Animation("resources/sprites/pressed_plate.png", 40, 8);
            animationDefault = new Animation("resources/sprites/released_plate.png", 40, 8);
            mechanisms = new List<IMechanism>();
            
            SetAnimation(animationDefault);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void Use(IActor actor)
        {   
            if (actor == null)
                return;

            foreach (IMechanism mechanism in mechanisms)
            {
                if (IntersectsWithActor(actor))
                    Press(mechanism);

                else
                {
                    intersector = null;
                    Release(mechanism);
                }
            }

            GetAnimation().Start();
        }

        public void SetMechanisms(List<IMechanism> mechanisms)
        {
            this.mechanisms = this.mechanisms.Concat(mechanisms);
        }

        public void Press(IMechanism mechanism)
        {
            SetAnimation(animationPressed);
            mechanism.Activate();
        }

        public void Release(IMechanism mechanism)
        {
            SetAnimation(animationDefault);
            mechanism.Deactivate();
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