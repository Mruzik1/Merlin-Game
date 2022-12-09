using Merlin2d.Game;


namespace MyGame.Spells
{
    public class SpellDirector : ISpellDirector
    {
        private Dictionary<string, int> effectsCost;
        private Dictionary<string, SpellInfo> spellsInfo;
        private ISpellBuilder builder;
        private IWizard wizard;

        public SpellDirector(IWizard wizard)
        {
            SpellDataProvider provider = SpellDataProvider.GetInstance();

            spellsInfo = provider.GetSpellInfo();
            effectsCost = provider.GetSpellEffects();
            
            this.wizard = wizard;
        }

        public ISpell Build(string spellName)
        {
            SpellEffectFactory effectFactory = new SpellEffectFactory();
            SpellInfo spell = spellsInfo[spellName];
            int cost = 0;

            // create a builder
            if (spell.SpellType == SpellType.Projectile)
                builder = new ProjectileSpellBuilder(effectsCost);
            else
                builder = new SelfCastSpellBuilder(effectsCost);

            // add effects, count total cost
            foreach(string rawName in spell.EffectNames)
            {
                string[] name = rawName.Split('-');

                cost += effectsCost[name[0]];
                builder.AddEffect(rawName);
            }

            // if a wizard has no mana
            if (wizard.GetMana() < cost)
                return null;

            // load animation if it's a projectile spell, could not do it earlier because it would cause some difficulties
            if (spell.AnimationPath != "")
            {
                Animation animation = new Animation(spell.AnimationPath, spell.AnimationWidth, spell.AnimationHeight);
                builder.SetAnimation(animation);
            }

            // reduce mana, set the spell cost
            wizard.ChangeMana(-cost);
            builder.SetSpellCost(cost);

            return builder.CreateSpell(wizard);
        }
    }
}