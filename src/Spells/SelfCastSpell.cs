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
            // int totalCost = 0;

            // foreach (ICommand effect in effects)
            //     totalCost += (effect as AbstractEffect).GetCost();

            // return (int)Math.Round(totalCost * 0.7);

            return (int)Math.Round(cost*0.7);
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