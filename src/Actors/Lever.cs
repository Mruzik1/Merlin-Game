using Merlin2d.Game;
using Merlin2d.Game.Actors;


namespace MyGame.Actors
{
    public class Lever : AbstractActor, IController
    {
        private IMechanism mechanism;
        private Animation animationActive;
        private Animation animationInactive;

        public Lever(int x, int y)
        {
            animationActive = new Animation("resources/sprites/switch_on.png", 25, 25);
            animationInactive = new Animation("resources/sprites/switch_off.png", 25, 25);
            
            SetAnimation(animationInactive);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void Use(IActor actor)
        {   
            if (!mechanism.IsActivated())
            {
                mechanism.Activate();
                SwitchOn();
            }
            else
            {
                mechanism.Deactivate();
                SwitchOff();
            }

            GetAnimation().Start();
        }

        public void SetMechanism(IMechanism mechanism)
        {
            this.mechanism = mechanism;
        }

        public void SwitchOn()
        {
            SetAnimation(animationActive);
        }

        public void SwitchOff()
        {
            SetAnimation(animationInactive);
        }

        public override void Update() {}
    }
}