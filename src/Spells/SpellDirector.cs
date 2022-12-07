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

            if (spell.SpellType == SpellType.Projectile)
            {
                builder = new ProjectileSpellBuilder(effectsCost);
                Animation animation = new Animation(spell.AnimationPath, spell.AnimationWidth, spell.AnimationHeight);
                builder.SetAnimation(animation);
            }
            
            if (spell.SpellType == SpellType.SelfCast)
                builder = new SelfCastSpellBuilder(effectsCost);

            foreach(string rawName in spell.EffectNames)
            {
                string[] name = rawName.Split('-');

                cost += effectsCost[name[0]];
                builder.AddEffect(rawName);
            }

            builder.SetSpellCost(cost);

            return builder.CreateSpell(wizard);
        }
    }
}