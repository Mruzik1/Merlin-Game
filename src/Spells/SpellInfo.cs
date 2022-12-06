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

            return new SpellInfo
            {
                Name = values[0],
                SpellType = values[1] == "projectile" ? SpellType.Projectile : SpellType.SelfCast,
                AnimationPath = values[2],
                AnimationWidth = int.Parse(values[3]),
                AnimationHeight = int.Parse(values[4]),
                EffectNames = values[5].Split(',')
            };
        }
    }
}