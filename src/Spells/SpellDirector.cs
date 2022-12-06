using MyGame.Commands;
using MyGame.Actors;
using Merlin2d.Game;


namespace MyGame.Spells
{
    public class SpellDirector : ISpellDirector
    {
        private Dictionary<string, ICommand> effectNames;
        private Dictionary<string, int> effectsCost;
        private Dictionary<string, SpellInfo> spellsInfo;
        private ISpellBuilder builder;
        private IWizard wizard;

        public SpellDirector(IWizard wizard)
        {
            SpellDataProvider provider = SpellDataProvider.GetInstance();

            // filling effects info
            effectNames = new Dictionary<string, ICommand>();
            effectNames.Add("Slowdown", new SpeedEffect(new SlowSpeedStrtegy(), 10000));
            effectNames.Add("Heal Over Time", new HealthOverTime(5, 5, 1000));
            effectNames.Add("Damage", new HealthEffect(-30));

            // filling effects cost
            effectsCost = new Dictionary<string, int>();
            effectsCost.Add("Slowdown", 5);
            effectsCost.Add("Heal Over Time", 15);
            effectsCost.Add("Damage", 10);

            // filling spells info
            spellsInfo = new Dictionary<string, SpellInfo>();

            spellsInfo.Add("Painful Slowdown", new SpellInfo());
            spellsInfo["Painful Slowdown"].AnimationHeight = 56;
            spellsInfo["Painful Slowdown"].AnimationWidth = 56;
            spellsInfo["Painful Slowdown"].AnimationPath = "resources/sprites/crystal_off.png";
            spellsInfo["Painful Slowdown"].EffectNames = new List<string>();
            spellsInfo["Painful Slowdown"].EffectNames = spellsInfo["Painful Slowdown"].EffectNames.Append("Slowdown");
            spellsInfo["Painful Slowdown"].EffectNames = spellsInfo["Painful Slowdown"].EffectNames.Append("Damage");
            spellsInfo["Painful Slowdown"].SpellType = SpellType.Projectile;
            spellsInfo["Painful Slowdown"].Name = "Painful Slowdown";

            spellsInfo.Add("Healing", new SpellInfo());
            spellsInfo["Healing"].EffectNames = new List<string>();
            spellsInfo["Healing"].EffectNames = spellsInfo["Healing"].EffectNames.Append("Heal Over Time");
            spellsInfo["Healing"].SpellType = SpellType.SelfCast;
            spellsInfo["Healing"].Name = "Healing";

            this.wizard = wizard;
        }

        public ISpell Build(string spellName)
        {
            SpellInfo spell = spellsInfo[spellName];
            int cost = 0;

            if (spell.SpellType == SpellType.Projectile)
            {
                builder = new ProjectileSpellBuilder(effectsCost, effectNames);
                Animation animation = new Animation(spell.AnimationPath, spell.AnimationWidth, spell.AnimationHeight);
                builder.SetAnimation(animation);
            }
            
            if (spell.SpellType == SpellType.SelfCast)
                builder = new SelfCastSpellBuilder(effectsCost, effectNames);

            foreach(string name in spell.EffectNames)
            {
                cost += effectsCost[name];
                builder.AddEffect(name);
            }
            builder.SetSpellCost(cost);

            return builder.CreateSpell(wizard);
        }
    }
}