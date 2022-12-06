


namespace MyGame.Spells
{
    public class SpellEffect
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public class SpellDataProvider : ISpellDataProvider
    {
        private static SpellDataProvider instance;
        private Dictionary<string, SpellInfo> spellInfos;
        private Dictionary<string, int> effectInfos;

        private SpellDataProvider()
        {

        }

        public static SpellDataProvider GetInstance()
        {
            if (instance == null)
                instance = new SpellDataProvider();
            return instance;
        }

        public Dictionary<string, SpellInfo> GetSpellInfo()
        {
            if (spellInfos == null)
                LoadSpellInfo();
            return spellInfos;
        }

        private void LoadSpellInfo()
        {
            string[] lines = File.ReadAllLines("resources/spells/spell.csv");
            spellInfos = new Dictionary<string, SpellInfo>();

            foreach (string line in lines[1..])
            {
                spellInfos.Add(line.Split(';')[0], line);
            }
        }

        public Dictionary<string, int> GetSpellEffects()
        {
            return effectInfos;
        }

        private void LoadEffectsInfo()
        {
            effectInfos = new Dictionary<string, int>();
            string json = File.ReadAllText("resources/spells/effects.json");
        }
    }
}