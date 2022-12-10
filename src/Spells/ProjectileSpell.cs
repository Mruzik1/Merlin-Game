using MyGame.Actors;
using MyGame.Commands;
using Merlin2d.Game;


namespace MyGame.Spells
{
    public class ProjectileSpell: AbstractActor, IMovable, ISpell
    {
        private ISpeedStrategy speedStrategy;
        private IWizard wizard;
        private Move move;
        private IEnumerable<ICommand> effects;
        private int cost;

        public ProjectileSpell(IWizard wizard, int cost, Animation animation)
        {
            this.effects = new List<ICommand>();
            this.cost = cost;
            this.wizard = wizard;
            
            SetSpeedStrategy(new NormalSpeedStrategy());
            SetPosition(wizard.GetX(), wizard.GetY());

            SetAnimation(animation);
            GetAnimation().Start();

            if ((wizard as AbstractCharacter).GetDirection() == ActorOrientation.FacingLeft)
            {
                move = new Move(this, -1, 0);
                animation.FlipAnimation();
            }
            else 
                move = new Move(this, 1, 0);
        }


        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.speedStrategy = speedStrategy;
        }

        public double GetSpeed()
        {
            return speedStrategy.GetSpeed(3);
        }

        public override void Update()
        {
            int oldX = GetX();
            move.Execute();

            // if facing a wall
            if (GetX() == oldX)
            {
                RemoveFromWorld();
                return;
            }

            GetWorld().GetActors().ForEach(actor => {
                if (actor.GetName() != "Merlin" && actor is ICharacter &&
                    IntersectsWithActor(actor))
                {
                    ApplyEffects((ICharacter)actor);
                    RemoveFromWorld();
                }
            });
        }

        public ISpell AddEffect(ICommand effect)
        {
            effects = effects.Append(effect);

            return this;
        }

        public void AddEffects(IEnumerable<ICommand> effects)
        {
            foreach(ICommand effect in effects)
                AddEffect(effect);
        }

        public int GetCost()
        {
            return cost;
        }

        public void ApplyEffects(ICharacter target)
        {
            foreach (ICommand effect in effects)
                (effect as AbstractEffect).SetTarget(target);
        }
    }
}