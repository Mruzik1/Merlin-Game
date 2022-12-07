using MyGame.Actors;
using MyGame.Commands;
using Merlin2d.Game;


namespace MyGame.Spells
{
    public class ProjectileSpell: AbstractActor, IMovable, ISpell
    {
        private ISpeedStrategy speedStrategy;
        private IWizard wizard;
        private IEnumerable<ICommand> effects;
        private int cost;

        public ProjectileSpell(IWizard wizard, int cost, Animation animation)
        {
            this.effects = new List<ICommand>();
            this.cost = cost;
            this.wizard = wizard;
            
            SetAnimation(animation);
            SetPosition(wizard.GetX(), wizard.GetY());
            GetAnimation().Start();
        }


        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.speedStrategy = speedStrategy;
        }

        public double GetSpeed()
        {
            return speedStrategy.GetSpeed(2);
        }

        public override void Update()
        {
            
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
            if (wizard.GetMana() >= GetCost())
            {
                foreach (ICommand effect in effects)
                    (effect as AbstractEffect).SetTarget(target);
                    
                wizard.ChangeMana(-GetCost());
            }
        }
    }
}