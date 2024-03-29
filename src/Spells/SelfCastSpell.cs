using MyGame.Commands;
using MyGame.Actors;


namespace MyGame.Spells
{
    public class SelfCastSpell: ISpell
    {
        private IWizard wizard;
        private IEnumerable<ICommand> effects;
        private int cost;

        public SelfCastSpell(IWizard wizard, int cost)
        {
            this.effects = new List<ICommand>();
            this.cost = cost;
            this.wizard = wizard;
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