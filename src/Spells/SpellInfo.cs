namespace MyGame.Spells
{
    public class SpellInfo
    {
        public string Name { get; set; }
        public SpellType SpellType { get; set; }
        public IEnumerable<string> EffectNames { get; set; }
        public string AnimationPath { get; set; }
        public int AnimationWidth { get; set; }
        public int AnimationHeight { get; set; }

        public static implicit operator SpellInfo(string data)
        {
            string[] values = data.Split(';');

            string name = "";
            string animationPath = "";
            int animationWidth = 0;
            int animationHeight = 0;
            SpellType spellType = SpellType.SelfCast;
            IEnumerable<string> effectNames = new List<string>();

            try
            {
                name = values[0];
                spellType = values[1] == "projectile" ? SpellType.Projectile : SpellType.SelfCast;
                animationPath = values[2];
                animationWidth = int.Parse(values[3]);
                animationHeight= int.Parse(values[4]);
                effectNames = values[5].Split(',');
            }
            catch (ArgumentException e)
            {
                Console.Error.WriteLine($"Wrong spell values: {e}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected exception: {e}");
            }
            
            return new SpellInfo
            {
                Name = name,
                SpellType = spellType,
                AnimationPath = animationPath,
                AnimationWidth = animationWidth,
                AnimationHeight = animationHeight,
                EffectNames = effectNames
            };
        }
    }
}