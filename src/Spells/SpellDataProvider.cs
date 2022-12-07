using Newtonsoft.Json;
using System;


namespace MyGame.Spells
{
    public class SpellDataProvider : ISpellDataProvider
    {
        private static SpellDataProvider instance;
        private Dictionary<string, SpellInfo> spellsInfo;
        private Dictionary<string, int> effectsInfo;

        private SpellDataProvider()
        {

        }

        internal class SpellEffect
        {
            public string Name { get; set; }
            public int Cost { get; set; }
        }

        public static SpellDataProvider GetInstance()
        {
            if (instance == null)
                instance = new SpellDataProvider();
            return instance;
        }

        public Dictionary<string, SpellInfo> GetSpellInfo()
        {
            if (spellsInfo == null)
                LoadSpellInfo();
            return spellsInfo;
        }

        private void LoadSpellInfo()
        {
            string[] lines = File.ReadAllLines("resources/spells/spell.csv");
            spellsInfo = new Dictionary<string, SpellInfo>();

            foreach (string line in lines[1..])
            {
                spellsInfo.Add(line.Split(';')[0], line);
            }
        }

        public Dictionary<string, int> GetSpellEffects()
        {
            if (effectsInfo == null)
                LoadEffectsInfo();
            return effectsInfo;
        }

        private void LoadEffectsInfo()
        {
            string json = File.ReadAllText("resources/spells/effects.json");
            List<SpellEffect> effects = JsonConvert.DeserializeObject<List<SpellEffect>>(json);

            effectsInfo = new Dictionary<string, int>();
            foreach (SpellEffect effect in effects)
            {
                effectsInfo.Add(effect.Name, effect.Cost);
            }
        }
    }
}