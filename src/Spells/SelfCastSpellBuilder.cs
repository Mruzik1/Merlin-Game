using Merlin2d.Game;
using MyGame.Commands;


namespace MyGame.Spells
{
    public class SelfCastSpellBuilder : ISpellBuilder
    {
        private Dictionary<string, int> effectsCost;
        private SpellEffectFactory effectFactory;
        private List<ICommand> effects;
        private int cost;

        public SelfCastSpellBuilder(Dictionary<string, int> effectsCost)
        {
            this.effectsCost = effectsCost;
            this.effects = new List<ICommand>();
            this.effectFactory = new SpellEffectFactory();
        }

        public ISpellBuilder AddEffect(string effectName)
        {
            string[] name = effectName.Split('-');
            ICommand effect = effectFactory.Create(name);

            effects.Add(effect);
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