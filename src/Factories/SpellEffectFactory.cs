using MyGame.Actors;
using MyGame.Commands;


namespace MyGame.Spells
{
    public class SpellEffectFactory
    {
        public ICommand Create(string[] effectName)
        {
            ICommand effect = null;

            if (effectName[0] == "directdamage")
            {
                effect = new HealthEffect(effectName[0]);
                (effect as HealthEffect).SetHp(-int.Parse(effectName[1]));
            }

            else if (effectName[0] == "healovertime")
            {
                effect = new HealthOverTime(effectName[0]);
                (effect as HealthOverTime).SetHp(int.Parse(effectName[1]));
                (effect as HealthOverTime).SetSteps(int.Parse(effectName[2]));
                (effect as HealthOverTime).SetDuration(int.Parse(effectName[3]));
            }

            else if (effectName[0] == "speeddecrease")
            {
                effect = new SpeedEffect(effectName[0], new SlowSpeedStrtegy());
                (effect as SpeedEffect).SetDuration(int.Parse(effectName[1]));
            }

            return effect;
        }
    }
}