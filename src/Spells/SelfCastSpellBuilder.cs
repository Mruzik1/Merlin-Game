using Merlin2d.Game;
using MyGame.Commands;


namespace MyGame.Spells
{
    public class SelfCastSpellBuilder : ISpellBuilder
    {
        private Dictionary<string, int> effectsCost;
        private Dictionary<string, ICommand> effectNames;
        private List<ICommand> effects;
        private int cost;

        public SelfCastSpellBuilder(Dictionary<string, int> effectsCost, Dictionary<string, ICommand> effectNames)
        {
            this.effectsCost = effectsCost;
            this.effectNames = effectNames;

            this.effects = new List<ICommand>();
        }

        public ISpellBuilder AddEffect(string effectName)
        {
            effects.Add(effectNames[effectName]);
            return this;
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            return this;
        }

        public ISpellBuilder SetSpellCost(int cost)
        {
            this.cost = cost;
            return this;
        }

        public ISpell CreateSpell(IWizard wizard)
        {
            ISpell spell = new SelfCastSpell(wizard, cost);
            spell.AddEffects(effects);

            return spell;
        }
    }
}