using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public class Lever : AbstractActor, IController
    {
        private IEnumerable<IMechanism> mechanisms;
        private Animation animationActive;
        private Animation animationInactive;

        public Lever(int x, int y)
        {
            animationActive = new Animation("resources/sprites/switch_on.png", 25, 25);
            animationInactive = new Animation("resources/sprites/switch_off.png", 25, 25);
            mechanisms = new List<IMechanism>();
            
            SetAnimation(animationInactive);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void Use(IActor actor)
        {   
            foreach (IMechanism mechanism in mechanisms)
            {
                if (!mechanism.IsActivated())
                    SwitchOn(mechanism);
                else
                    SwitchOff(mechanism);
            }

            GetAnimation().Start();
        }

        public void SetMechanisms(List<IMechanism> mechanisms)
        {
            this.mechanisms = this.mechanisms.Concat(mechanisms);
        }

        public void SwitchOn(IMechanism mechanism)
        {
            SetAnimation(animationActive);
            mechanism.Activate();
        }

        public void SwitchOff(IMechanism mechanism)
        {
            SetAnimation(animationInactive);
            mechanism.Deactivate();
        }

        public override void Update() {}
    }
}